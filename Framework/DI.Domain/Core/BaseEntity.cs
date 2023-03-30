using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using DI.Attributes;
using DI.Domain.Services;

namespace DI.Domain.Core
{
    public abstract class BaseEntity : IEntity, IEntityEvent
    {
        [Key] [NoPatch] [Column(Order = 0)] public long Id { get; set; }

        [Required]
        [NoPatch]
        [Column(Order = 996)]
        public bool Locked { get; set; }

        [Required]
        [NoPatch]
        [Column(Order = 997)]
        public bool Disabled { get; set; }

        [Required]
        [NoPatch]
        [Column(Order = 998)]
        public bool Deleted { get; set; }

        [Timestamp]
        [NoPatch]
        [Column(Order = 999)]
        public byte[] Timestamp { get; set; }


        public bool IsTransient => Id == default;

        public virtual string GetName()
        {
            return GetType().Name.ToLower();
        }

        public virtual string GetKey()
        {
            return $"{Id}";
        }

        public EntityReference Reference()
        {
            return new EntityReference(Id, GetType().Name);
        }

        public virtual async Task<IEntity> OnCoreEvent(EntityEvent @event, IDataStore store)
        {
            await Task.Delay(0);
            return null;
        }
    }
}