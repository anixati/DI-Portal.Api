using System.Linq;
using Di.Qry.Contracts;
using Di.Qry.Core;

namespace Di.Qry.Schema
{
    public abstract class QrySchemaBase : IQrySchema
    {
        public abstract string SchemaName { get; }
        public virtual bool RefData => false;

        public IQryState Create()
        {
            var entity = CreateEntity();
            var qs = new QryState(entity);
            SetupQuery(entity, qs);
            ConfigureQry(qs);
            return qs;
        }

        protected virtual void ConfigureQry(IQryState qs)
        {
        }

        private static void SetupQuery(Entity entity, QryState qState)
        {
            qState.AddSelect(entity);
            if (!entity.Links.Any()) return;
            foreach (var qryLink in entity.Links.Values)
            {
                qState.Join(qryLink);
                SetupQuery(qryLink.Entity, qState);
            }
        }

        protected abstract Entity CreateEntity();
    }
}