using Boards.Services.Core;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Access.Lists
{
    public class UserList : QrySchema
    {
        public override string SchemaName => "UserList";
        public override string Title => "Current Users";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.UsersView);
           // pt.Select("UserId");
            pt.Column("FullName", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.UserAdmin.Path();
            });
            pt.AddSearchCols( "Email");
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



    public class UserRoleList : QrySchema
    {
        public override string SchemaName => "UserRoleList";
        public override string Title => "Current Roles";

        protected override Table CreateEntity()
        {

            var tb = Table.Create("UserRoles", "rls", "", "acl");


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


            var pt = Table.Create(Constants.Db.UsersView);
            // pt.Select("UserId");
            pt.Column("FullName", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.UserAdmin.Path();
            });
            pt.AddSearchCols("Email");
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