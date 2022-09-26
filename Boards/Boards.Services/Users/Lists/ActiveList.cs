using Boards.Services.Client;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Users.Lists
{

    public class ActiveList : QrySchema
    {
        public override string SchemaName => "ActiveUsers";
        public override string Title => "Current Users";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.UsersView);
            pt.Column("FullName", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "Id";
                x.LinkPath = Routes.Users.Path();

            });
            pt.AddSearchCols("Phone", "Email");
            pt.AddDateColumn("CreatedOn");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("FullName", false);
        }
    }
    
}