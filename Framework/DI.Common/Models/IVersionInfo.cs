using System.Collections.Generic;

namespace DI.Models
{
    public interface IVersionInfo
    {
        string Name { get; set; }
        string TimeStamp { get; set; }
        List<string> Version { get; set; }
    }
}