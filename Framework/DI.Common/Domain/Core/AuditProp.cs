namespace DI.Domain.Core
{
    public class AuditProp
    {
        public AuditProp(string key)
        {
            Key = key;
        }

        public string Key { get; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}