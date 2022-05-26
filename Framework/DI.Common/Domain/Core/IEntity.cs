namespace DI.Domain.Core
{
    public interface IEntity
    {
        long Id { get; set; }
        bool Locked { get; set; }
        bool Disabled { get; set; }
        bool Deleted { get; set; }
        bool IsTransient { get; }
        byte[] Timestamp { get; set; }
        string GetName();
        string GetKey();
    }
}