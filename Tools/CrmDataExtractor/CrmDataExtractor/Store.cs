using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CrmDataExtractor
{
    public class Store
    {
        public Dictionary<string, Dictionary<string,string>> Options { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public Dictionary<string, Dictionary<string, string>> Boards { get; set; } = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> Roles { get; set; } = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> Appointments { get; set; } = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> Contacts { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public Dictionary<string, Dictionary<string, string>> Users { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public Dictionary<string, Dictionary<string, string>> Portfolios { get; set; } = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> Ministers { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public void Save()
        {
            var path = $"c:\\temp\\{Guid.NewGuid().ToString("N")}.json";
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
            Process.Start(path);
        }
    }
}


