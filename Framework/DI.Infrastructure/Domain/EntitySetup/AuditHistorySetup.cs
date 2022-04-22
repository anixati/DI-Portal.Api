using DI.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DI.Domain.EntitySetup
{
    public class AuditHistorySetup : IEntityTypeConfiguration<AuditHistory>
    {
        public void Configure(EntityTypeBuilder<AuditHistory> builder)
        {
            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(e => e.Data).HasJsonConversion();
        }
    }
}