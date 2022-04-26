using Boards.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boards.Infrastructure.Domain.Config
{
    public class RoleEventConfig : IEntityTypeConfiguration<BoardRoleEvent>
    {
        public void Configure(EntityTypeBuilder<BoardRoleEvent> builder)
        {
        }
    }
}