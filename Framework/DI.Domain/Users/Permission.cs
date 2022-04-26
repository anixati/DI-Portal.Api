using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("Permissions", Schema = Constants.SecuritySchema)]
    public class Permission : IntersectBaseEntity
    {
        [Required] public long AppResourceId { get; set; }

        public AppResource AppResource { get; set; }

        [Required] public long AppRoleId { get; set; }

        public AppRole AppRole { get; set; }

        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public int Mask { get; set; }
    }
}