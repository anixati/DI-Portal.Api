using System;

namespace DI.Domain.Core
{
    public interface IAuditEntity : IEntity
    {
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }
        DateTime ModifiedOn { get; set; }
        string ModifiedBy { get; set; }
    }

    public interface INamedEntity : IAuditEntity
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    public interface ICheckSystemEntity : IAuditEntity
    {
        bool IsSystem { get; set; }
    }
}