using Di.Qry.Core;
using Di.Qry.Schema.Types;

namespace Di.Qry.Schema
{
    public abstract class QrySchema : IQrySchema
    {
        public abstract string SchemaName { get; }
        public abstract string Title { get; }
        public virtual SchemaType SchemaType => SchemaType.DataQuery;

        public virtual IQryState Create()
        {
            var entity = CreateEntity();
            var (name, desc) = GetDefaultSort();
            entity.AddSortCol(name, desc);
            var qs = QryState.Create(entity);
            ConfigureQry(qs);
            qs.FinaliseCreate();
            qs.Title = Title;
            return qs;
        }

        protected virtual void ConfigureQry(QryState qs)
        {
        }

        protected abstract Entity CreateEntity();
        protected abstract (string, bool) GetDefaultSort();
    }
}