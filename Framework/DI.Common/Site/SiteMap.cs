using System.Collections.Generic;

namespace DI.Site
{
    public class SiteMap
    {
        public bool Restricted { get; set; }
        public string Logo { get; set; }
        public string Icon { get; set; }
        public List<NavLink> Navigation { get; set; } = new();
        public List<NavLink> Footer { get; set; } = new();
    }
}