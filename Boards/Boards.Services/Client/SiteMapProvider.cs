using Boards.Domain;
using DI.Domain.Users;
using DI.Extensions;
using DI.Security;
using DI.Site;
using System.Threading.Tasks;

namespace Boards.Services.Client
{
    public class SiteMapProvider : ISiteMapProvider
    {
        private readonly IIdentity _user;
        private readonly IBoardsContext _boardsContext;
        public SiteMapProvider(IIdentityProvider provider, IBoardsContext boardsContext)
        {
            _user = provider.GetIdentity();
            _boardsContext = boardsContext;
        }

        public async Task<SiteMap> Create()
        {
            var rv = new SiteMap
            {
                Logo = "Boards",
                Restricted = true,
            };
            if(_user== null) return rv;

            var usrRepo = _boardsContext.Repo<AppUser>();
            if(!long.TryParse(_user.UserId,out var userId)) return rv;

            var user = await usrRepo.FindAsync(userId, false);
            if (user == null) return rv;
            if(!user.AccessGranted.HasValue) return rv;
            if (!_user.HasRoles()) return rv;
            rv.Restricted = false;
            AddBoards(rv);
            if (_user.IsAdmin())
            {
                AddReports(rv);
                AddAdmin(rv);
            }
            return rv;
        }

        private static void AddBoards(SiteMap rv)
        {
            var link = new NavLink(Routes.Base, "Boards");
            link.Add(Routes.Dashboard.Key, "Dashboard");
            link.Add(Routes.Boards.Key, "Boards");
            link.Add(Routes.Group, "Contacts", x =>
            {
                x.Add(Routes.Appointee.Key, "Appointees");
                x.Add(Routes.Minister.Key, "Ministers");
                x.Add(Routes.Secretary.Key, "Secretaries");
            });
            link.Add(Routes.Portfolios.Key, "Portfolios");
            link.Add(Routes.Users.Key, "Users");
            link.Add(Routes.Skills.Key, "Skills");
            rv.Navigation.Add(link);
        }

        private static void AddReports(SiteMap rv)
        {
            var link = new NavLink(Routes.Reports, "Reports");
            link.Add(Routes.ReportDashboard.Key, "Board Data");


            rv.Navigation.Add(link);
        }

        private void AddAdmin(SiteMap rv)
        {
            var link = new NavLink(Routes.Admin, "Admin");
            link.Add(Routes.AdminDashboard.Key, "Dashboard");
            if (_user.IsSysAdmin()) link.Add("options", "Options");
            link.Add("logs", "Audit logs");

            if (_user.IsSysAdmin())
                link.Add(Routes.Group, "Security", x =>
                {
                    x.Add("users", "Users");
                    x.Add("roles", "Roles");
                    x.Add("teams", "Teams");
                });
            rv.Navigation.Add(link);
        }

    }
}