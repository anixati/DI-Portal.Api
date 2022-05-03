namespace DI.Domain.Core
{
    public abstract class BaseViewModel : IViewModel
    {
        public bool IsLocked { get; set; }
        public bool IsDisabled { get; set; }
        public long Id { get; set; }
    }
}