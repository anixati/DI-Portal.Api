using Boards.Services.Core;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Access.Lists
{
    public class RolesList : QrySchema
    {
        public override string SchemaName => "RolesList";
        public override string Title => "Application Roles";
        protected override Table CreateEntity()
        {
            var tb = Table.Create("Roles", "rls","","acl");
            tb.Column("Name", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.RoleAdmin.Path();
            });
            tb.AddSearchCols("Code");
            tb.AddDateColumn("CreatedOn");
            return tb;
        }
        protected override void ConfigureQry(QryState qs)
        {
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }
}