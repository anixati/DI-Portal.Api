using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Di.Qry.Contracts;
using Di.Qry.Providers;
using Di.Qry.Requests;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace DI.Services.Queries
{
    public class ConfigHandler : ServiceBase, IRequestHandler<ConfigRequest, ConfigResponse>
    {
        private readonly IQryProvider _provider;

        public ConfigHandler(IQryProvider provider, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _provider = provider;
        }

        public async Task<ConfigResponse> Handle(ConfigRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            return string.IsNullOrEmpty(request.SchemaName)
                ? new ConfigResponse(_provider.GetSchemas())
                : new ConfigResponse(_provider.GetConfig(request.SchemaName));
        }
    }


    
}
