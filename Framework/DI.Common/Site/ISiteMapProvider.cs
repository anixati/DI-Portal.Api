using System.Threading.Tasks;

namespace DI.Site
{
    public interface ISiteMapProvider
    {
        Task<SiteMap> Create();
    }
}