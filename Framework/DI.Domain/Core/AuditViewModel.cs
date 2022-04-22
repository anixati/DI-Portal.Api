namespace DI.Domain.Core
{
    public abstract class AuditViewModel : BaseViewModel
    {
        public string CreatedByStamp { get; set; }
        public string ModifiedByStamp { get; set; }
    }
}