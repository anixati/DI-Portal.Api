using DI.Forms.Types;

namespace Boards.Services.Core
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
        public static readonly IClientRoute Minister = Client.New("ministers");
        public static readonly IClientRoute Secretary = Client.New("secretaries");
        public static readonly IClientRoute Portfolios = Client.New("portfolios");
        public static readonly IClientRoute Groups = Client.New("groups");
        public static readonly IClientRoute Users = Client.New("appusers");

        public static readonly IClientRoute ReportDashboard = Client.New("dashboard", Reports);
        public static readonly IClientRoute AdminDashboard = Client.New("dashboard", Admin);
    }

    public class Client : IClientRoute
    {
        private string _base;
        public Client(string key, string @base)
        {
            Key = key;
            _base = @base;
        }

        public string Key { get; }
        public string Path()
        {
            return string.IsNullOrEmpty(Key) ? "" : $"/{_base}/{Key}";
        }

        public static IClientRoute New(string key,string cb= Routes.Base)
        {
            return new Client(key,cb);
        }
    }
}