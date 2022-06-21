using Boards.Services.Core;
using DI.Forms.Types;

namespace Boards.Services
{
    public static class Routes
    {
        public const string Group = "";
        public const string Base = "boards";
        public const string Reports = "reports";
        public const string Admin = "admin";

        public static readonly IClientRoute Default = Client.New("");

        public static readonly IClientRoute Dashboard = Client.New("dashboard");
        public static readonly IClientRoute Boards = Client.New("list");
        public static readonly IClientRoute Roles = Client.New("brdroles");

        public static readonly IClientRoute Appointee = Client.New("appointees");
        public static readonly IClientRoute Appointment = Client.New("appointments");
        public static readonly IClientRoute Minister = Client.New("ministers");
        public static readonly IClientRoute Secretary = Client.New("secretaries");
        public static readonly IClientRoute Portfolios = Client.New("portfolios");
        public static readonly IClientRoute Skills = Client.New("skills");
        public static readonly IClientRoute Groups = Client.New("groups");
        public static readonly IClientRoute Users = Client.New("appusers");

        public static readonly IClientRoute ReportDashboard = Client.New("dashboard", Reports);
        public static readonly IClientRoute AdminDashboard = Client.New("dashboard", Admin);

        public static readonly IClientRoute UserAdmin = Client.New("users", Admin);
        public static readonly IClientRoute RoleAdmin = Client.New("roles", Admin);
        public static readonly IClientRoute TeamAdmin = Client.New("teams", Admin);
    }
}