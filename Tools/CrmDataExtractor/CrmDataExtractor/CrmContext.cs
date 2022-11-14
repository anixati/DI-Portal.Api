using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;

namespace CrmDataExtractor
{

    public class CrmContext
    {
        private bool _isDisposed;
        private IOrganizationService _orgService;
        private CrmServiceClient _client;

        private CrmContext()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var connStr = ConfigurationManager.AppSettings["CrmUrl"];
           // Console.WriteLine($"Connecting using {connStr}");
            _client = new CrmServiceClient($"{connStr}");
            if (_client.IsReady)
            {
                _orgService = (IOrganizationService)_client?.OrganizationWebProxyClient ?? _client?.OrganizationServiceProxy;
            }
            //throw new Exception($"{client.LastCrmError}");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public CrmServiceClient Client => _client;
        public IOrganizationService OrgService => _orgService;

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed) return;
            if (isDisposing)
                _client?.Dispose();
            _isDisposed = true;
        }

        public List<OptionSetMetadata> GetGlobalOptionSets()
        {
            var rv = new List<OptionSetMetadata>();
            var rsp = (RetrieveAllOptionSetsResponse)_orgService.Execute(new RetrieveAllOptionSetsRequest());
            if (rsp == null) return null;
            if (rsp.OptionSetMetadata == null || !rsp.OptionSetMetadata.Any()) return null; 

            foreach (var osd in rsp.OptionSetMetadata)
            {
                if (osd.OptionSetType == null || osd.OptionSetType != OptionSetType.Picklist) continue;
                if (!osd.Name.StartsWith("new_")) continue;
                rv.Add( (OptionSetMetadata)osd);
            }
            return rv;
        }


        public Dictionary<string, string> GetEntityOptionSets(string entity, string attributeName)
        {
            var rv = new Dictionary<string, string>();

            var attributeRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = entity,
                LogicalName = attributeName,
                RetrieveAsIfPublished = true
            };

            var attributeResponse = (RetrieveAttributeResponse)_orgService.Execute(attributeRequest);
            var rsp = (EnumAttributeMetadata)attributeResponse.AttributeMetadata;
            foreach (var x in rsp.OptionSet.Options)
            {
                var key = x.Label.UserLocalizedLabel.Label;
                rv[key] = $"{x.Value}";
            }
            return rv;
        }


        public Dictionary<string, Dictionary<string, string>> GetEntity(string entityName)
        {

            Console.WriteLine($" Getting {entityName} Data:");
            var rv = new Dictionary<string, Dictionary<string, string>>();
            var query = new QueryExpression(entityName)
            {
                ColumnSet =
                {
                    AllColumns = true
                }
            };
            var ec = _orgService.RetrieveMultiple(query);
            if (ec == null) return rv;
            foreach (var et in ec.Entities)
            {
                var key = et.Id.ToString("N");
                var avl = new Dictionary<string, string>();

                Console.Write(".");
                foreach (var atb in et.Attributes)
                {
                    var val = et.ToString(atb.Key);
                    avl[atb.Key] = val;
                }
                rv[key] = avl;
            }
            Console.WriteLine($"Done!");
            return rv;
        }






        public static CrmContext Create()
        {
            var cx = new CrmContext();
            var resp = cx.OrgService.Execute(new WhoAmIRequest()) as WhoAmIResponse;
            Console.WriteLine($"Connected \n\t User Id :{resp.UserId} \n\t Org Id:{resp.OrganizationId} \n\t Bu Id:{resp.BusinessUnitId}");
            var su =cx.Client.Retrieve("systemuser", resp.UserId,new ColumnSet(new string[] { "firstname", "lastname" }));
            Console.WriteLine($"Logged on as {su.Attributes["firstname"]} {su.Attributes["lastname"]}." );
            return cx;
        }
    }
}
