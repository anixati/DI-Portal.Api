namespace DI.Domain.Core
{
    public abstract class BaseViewModel : IViewModel
    {
        public long Id { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDisabled { get; set; }
    }
}