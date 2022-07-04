using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;

namespace DI.Domain.Activities
{
    public class Activity : ActivityBase
    {
        [Required,MaxLength(500)] public string Title { get; set; }
        [MaxLength(10000)] public string Notes { get; set; }

        [MaxLength(255)]
        public string ContentType { get; set; }
    }

    public class ActivityViewModel : ActivityModelBase
    {
        [Required, MaxLength(500)] public string Title { get; set; }
        [MaxLength(10000)] public string Notes { get; set; }

        [MaxLength(255)]
        public string ContentType { get; set; }
    }
}
