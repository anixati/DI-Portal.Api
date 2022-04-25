namespace DI.Domain.Core
{
    public abstract class BaseViewModel : IViewModel
    {
        public long Id { get; set; }
        public bool IsLocked { get;}
        public bool IsDisabled { get; }
    }
}