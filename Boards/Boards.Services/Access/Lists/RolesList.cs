using Boards.Services.Client;
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
            var tb = Table.Create("Roles", "pe","","acl");
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


    public class RoleUserList : QrySchema
    {
        public override string SchemaName => "RoleUserList";
        public override string Title => "Current Users";

        protected override Table CreateEntity()
        {

            var tb = Table.Create("UserRoles", "pe", "", "acl");
            tb.AddHiddenCols("AppUserId", "AppRoleId");
            var link = tb.InnerJoin("Users", "se", "Id", "AppUserId", lk =>
            {
                lk.CalColumn("CONCAT(se.FirstName,' ',se.LastName)", "Name", "Name", x =>
                {
                    x.Searchable = true;
                    x.Sortable = true;
                    x.Type = ColumnType.HyperLink;
                    x.LinkId = "AppUserId";
                    x.LinkPath = Routes.UserAdmin.Path();
                });
            }, "acl");
            return tb;
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.ParentId = "AppRoleId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("AppRoleId", false);
        }
    }


    public class RoleTeamList : QrySchema
    {
        public override string SchemaName => "RoleTeamList";
        public override string Title => "Current Teams";

        protected override Table CreateEntity()
        {

            var tb = Table.Create("TeamRoles", "pe", "", "acl");
            tb.AddHiddenCols("AppTeamId", "AppRoleId");
            var link = tb.InnerJoin("Teams", "se", "Id", "AppTeamId", lk =>
            {
                lk.CalColumn("Name", "Name", "Name", x =>
                {
                    x.Searchable = true;
                    x.Sortable = true;
                    x.Type = ColumnType.HyperLink;
                    x.LinkId = "AppTeamId";
                    x.LinkPath = Routes.TeamAdmin.Path();
                });
            }, "acl");
            return tb;
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.ParentId = "AppRoleId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("AppRoleId", false);
        }
    }

}