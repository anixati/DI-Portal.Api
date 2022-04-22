using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DI.Domain.Core
{
    public abstract class BaseEntity : IEntity
    {
        [Key] [Column(Order = 0)] public long Id { get; set; }

        [Required] [Column(Order = 996)] public bool Locked { get; set; }

        [Required] [Column(Order = 997)] public bool Disabled { get; set; }

        [Required] [Column(Order = 998)] public bool Deleted { get; set; }
        
        [Timestamp] [Column(Order = 999)] public byte[] Timestamp { get; set; }

        public bool IsTransient => Id == default;

        public string GetName()
        {
            return GetType().Name.ToLower();
        }
    }
}