using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;

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