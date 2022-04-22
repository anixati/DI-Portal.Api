using System.ComponentModel.DataAnnotations;
using DI.Core;

namespace DI.Actions
{
    public abstract class ActionBase : IDomainAction
    {
        [Required] [StringLength(255)] public string Name { get; set; }
    }
}