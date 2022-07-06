using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
