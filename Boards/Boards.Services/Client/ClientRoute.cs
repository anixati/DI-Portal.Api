using DI.Forms.Types;

namespace Boards.Services.Client
{
    public class ClientRoute : IClientRoute
    {
        private readonly string _base;
        public ClientRoute(string key, string @base)
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
            return new ClientRoute(key,cb);
        }
    }
}