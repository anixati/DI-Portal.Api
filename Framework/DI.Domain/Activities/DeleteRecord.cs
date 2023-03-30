using System.ComponentModel.DataAnnotations;

namespace DI.Domain.Activities
{
    public class DeleteRecord : ActivityBase
    {
        [MaxLength(500)] public string Notes { get; set; }
    }


    public class RecordViewModel : ActivityModelBase
    {
    }
}