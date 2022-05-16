namespace Di.Qry.Core
{
    public class Link
    {
        public Link(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public Entity Entity { get; set; }
        public LinkType LinkType { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}