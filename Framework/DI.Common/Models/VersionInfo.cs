using System.Collections.Generic;

namespace DI.Models
{
    public class VersionInfo : IVersionInfo
    {
        public string Name { get; set; }
        public string TimeStamp { get; set; }
        public List<string> Version { get; set; } = new();
    }
}