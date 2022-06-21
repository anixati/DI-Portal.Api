using Boards.Domain;
using Boards.Services.Core;
using DI.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Access.Handlers
{
    public class AppRoleHandler : BoardsFormHandler<AppRole>
    {
        public AppRoleHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "approle";
    }

    public class UserRoleHandler : BoardsFormHandler<UserRole>
    {
        public UserRoleHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }
        public override string SchemaKey => "userrole";
    }

    public class TeamRoleHandler : BoardsFormHandler<TeamRole>
    {
        public TeamRoleHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }
        public override string SchemaKey => "teamrole";
    }
    public class TeamUserHandler : BoardsFormHandler<TeamUser>
    {
        public TeamUserHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }
        public override string SchemaKey => "userrole";
    }
    public class PermissionHandler : BoardsFormHandler<Permission>
    {
        public PermissionHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }
        public override string SchemaKey => "permission";
    }



}