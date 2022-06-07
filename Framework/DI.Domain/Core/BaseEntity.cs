using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Attributes;

namespace DI.Domain.Core
{
    public abstract class BaseEntity : IEntity
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

        public virtual void OnPreUpdate(IEntity entity)
        {
            
        }
        
    }
}