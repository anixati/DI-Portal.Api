using Boards.Services.Client;
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
            pt.Column("FullName", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.UserAdmin.Path();
            });
            pt.AddSearchCols("UserId", "Email");
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

    public class InactiveUserList : QrySchema
    {
        public override string SchemaName => "InactiveUserList";
        public override string Title => "In-active Users";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.UsersView);
            pt.Column("FullName", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.UserAdmin.Path();
            });
            pt.AddSearchCols("UserId", "Email");
            pt.AddDateColumn("CreatedOn");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "1");
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
            var tb = Table.Create("UserRoles", "pe", "", "acl");
            tb.AddHiddenCols("AppUserId", "AppRoleId");
            var link = tb.InnerJoin("Roles", "se", "Id", "AppRoleId", x =>
            {
                x.Column("Name", "Name", "Name", x =>
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
            qs.ParentId = "AppUserId";
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("AppUserId", false);
        }
    }


    public class UserTeamList : QrySchema
    {
        public override string SchemaName => "UserTeamList";
        public override string Title => "Current Teams";

        protected override Table CreateEntity()
        {
            var tb = Table.Create("TeamUsers", "pe", "", "acl");
            tb.AddHiddenCols("AppUserId", "AppTeamId");
            var link = tb.InnerJoin("Teams", "se", "Id", "AppTeamId", x =>
            {
                x.Column("Name", "Name", "Name", x =>
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
            qs.ParentId = "AppUserId";
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("AppUserId", false);
        }
    }
}