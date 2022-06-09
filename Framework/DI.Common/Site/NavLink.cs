using System.Collections.Generic;

namespace DI.Site
{
    public class NavLink
    {
        public string Route { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public List<NavLink> Links { get; set; } = new();
    }
}