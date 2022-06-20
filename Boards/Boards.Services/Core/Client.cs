using DI.Forms.Types;

namespace Boards.Services.Core
{
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