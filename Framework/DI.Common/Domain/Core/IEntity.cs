using System.Threading.Tasks;
using DI.Domain.Services;

namespace DI.Domain.Core
{
    public interface IEntity
    {
        long Id { get; set; }
        bool Locked { get; set; }
        bool Disabled { get; set; }
        bool Deleted { get; set; }
        bool IsTransient { get; }
        byte[] Timestamp { get; set; }
        string GetName();
        string GetKey();
        EntityReference Reference();
    }

    public class EntityReference
    {
        public EntityReference(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }
        public string Name { get; }
    }


    public enum EntityEvent
    {
        Update = 0,
        Create,
        Delete
    }

    public interface IEntityEvent
    {
        Task<IEntity> OnCoreEvent(EntityEvent @event, IDataStore store);
    }
}