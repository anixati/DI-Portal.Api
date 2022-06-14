using DI.Site;

namespace Boards.Services.Core
{
    public class SiteMapProvider:ISiteMapProvider
    {
        public SiteMap Create()
        {
            var rv = new SiteMap {Logo = "Boards"};
            AddBoards(rv);
            AddReports(rv);
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
            rv.Navigation.Add(link);
        }

        private static void AddReports(SiteMap rv)
        {
            var link = new NavLink(Routes.Reports, "Reports");
            link.Add(Routes.ReportDashboard.Key, "Dashboard");
            rv.Navigation.Add(link);
        }

        private static void AddAdmin(SiteMap rv)
        {
            var link = new NavLink(Routes.Admin, "Admin");
            link.Add(Routes.AdminDashboard.Key, "Dashboard");
            link.Add("options", "Options");
            link.Add("logs", "Audit logs");
            link.Add(Routes.Group, "Security", x =>
            {
                x.Add("users", "users");
                x.Add("roles", "roles");
                x.Add("teams", "Teams");
            });
            rv.Navigation.Add(link);
        }
      
    }
}