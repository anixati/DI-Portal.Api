using DI.Extensions;
using DI.Security;
using DI.Site;

namespace Boards.Services.Client
{
    public class SiteMapProvider : ISiteMapProvider
    {
        private readonly IIdentity _user;

        public SiteMapProvider(IIdentityProvider provider)
        {
            _user = provider.GetIdentity();
        }

        public SiteMap Create()
        {
            var rv = new SiteMap
            {
                Logo = "Boards",
                Restricted = !_user.HasRoles()
            };
            if (rv.Restricted) return rv;
            AddBoards(rv);
            AddReports(rv);
            if (_user.IsAdmin())
                AddAdmin(rv);
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
            link.Add(Routes.ReportDashboard.Key, "Dashboard");
            rv.Navigation.Add(link);
        }

        private void AddAdmin(SiteMap rv)
        {
            var link = new NavLink(Routes.Admin, "Admin");
            link.Add(Routes.AdminDashboard.Key, "Dashboard");
            link.Add("options", "Options");
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