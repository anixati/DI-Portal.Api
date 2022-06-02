using Boards.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boards.Infrastructure.Domain.Config
{
    public class RoleEventConfig : IEntityTypeConfiguration<BoardAppointment>
    {
        public void Configure(EntityTypeBuilder<BoardAppointment> builder)
        {
        }
    }
}