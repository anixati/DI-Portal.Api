using System;
using System.Collections.Generic;

namespace DI.Site
{
    public class NavLink
    {
        public NavLink(string route, string label)
        {
            Route = $"/{route.ToLower().Trim()}";
            Label = label;
        }

        public string Route { get;  }
        public string Label { get;  }
        public string Icon { get; set; }
        public List<NavLink> Links { get; set; } = new();

        public NavLink Add(string route, string label,Action<NavLink> setup = null)
        {
            var link = new NavLink(route, label);
            setup?.Invoke(link);
            Links.Add(link);
            return this;
        }
    }
}