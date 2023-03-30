using DI.Forms.Types;

namespace Boards.Services.Client
{
    public static class Routes
    {
        public const string Group = "";
        public const string Base = "boards";
        public const string Reports = "reports";
        public const string Admin = "admin";

        public static readonly IClientRoute Default = ClientRoute.New("");

        public static readonly IClientRoute Dashboard = ClientRoute.New("dashboard");
        public static readonly IClientRoute Boards = ClientRoute.New("list");
        public static readonly IClientRoute Roles = ClientRoute.New("brdroles");

        public static readonly IClientRoute Appointee = ClientRoute.New("appointees");
        public static readonly IClientRoute Appointment = ClientRoute.New("appointments");
        public static readonly IClientRoute Minister = ClientRoute.New("ministers");
        public static readonly IClientRoute Secretary = ClientRoute.New("secretaries");
        public static readonly IClientRoute Portfolios = ClientRoute.New("portfolios");
        public static readonly IClientRoute Skills = ClientRoute.New("skills");
        public static readonly IClientRoute Groups = ClientRoute.New("groups");
        public static readonly IClientRoute Users = ClientRoute.New("appusers");

        public static readonly IClientRoute ReportDashboard = ClientRoute.New("dashboard");
        public static readonly IClientRoute AdminDashboard = ClientRoute.New("dashboard");

        public static readonly IClientRoute UserAdmin = ClientRoute.New("users");
        public static readonly IClientRoute RoleAdmin = ClientRoute.New("roles");
        public static readonly IClientRoute TeamAdmin = ClientRoute.New("teams");
    }
}