using Boards.Services.Client;
using Boards.Services.Core;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Access.Lists
{
    public class TeamsList : QrySchema
    {
        public override string SchemaName => "TeamsList";
        public override string Title => "Application Teams";
        protected override Table CreateEntity()
        {
            var tb = Table.Create("Teams", "pe","","acl");
            tb.Column("Name", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.TeamAdmin.Path();
            });
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




    public class TeamUserList : QrySchema
    {
        public override string SchemaName => "TeamUserList";
        public override string Title => "Current Users";

        protected override Table CreateEntity()
        {

            var tb = Table.Create("TeamUsers", "pe", "", "acl");
            tb.AddHiddenCols("AppUserId", "AppTeamId");
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
            qs.ParentId = "AppTeamId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("AppTeamId", false);
        }
    }


    public class TeamRoleList : QrySchema
    {
        public override string SchemaName => "TeamRoleList";
        public override string Title => "Current Roles";

        protected override Table CreateEntity()
        {

            var tb = Table.Create("TeamRoles", "pe", "", "acl");
            tb.AddHiddenCols("AppTeamId", "AppRoleId");
            var link = tb.InnerJoin("Roles", "se", "Id", "AppRoleId", lk =>
            {
                lk.CalColumn("Name", "Name", "Name", x =>
                {
                    x.Searchable = true;
                    x.Sortable = true;
                    x.Type = ColumnType.HyperLink;
                    x.LinkId = "AppRoleId";
                    x.LinkPath = Routes.RoleAdmin.Path();
                });
            }, "acl");
            return tb;
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.ParentId = "AppTeamId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("AppTeamId", false);
        }
    }
}