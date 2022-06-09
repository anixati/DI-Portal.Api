using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Site
{
    public interface ISiteMapProvider
    {
        SiteMap Create();
    }

    public class SiteMap
    {
        public string Logo { get; set; }
        public string Icon { get; set; }
        public List<NavLink> Navigation { get; set; } = new();
        public List<NavLink> Footer { get; set; } = new();

    }
}
