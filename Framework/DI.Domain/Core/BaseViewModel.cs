namespace DI.Domain.Core
{
    public abstract class BaseViewModel : IViewModel
    {
        public bool IsLocked { get; }
        public bool IsDisabled { get; }
        public long Id { get; set; }
    }
}