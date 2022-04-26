using Boards.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boards.Infrastructure.Domain.Config
{
    public class BoardRoleConfig : IEntityTypeConfiguration<BoardRole>
    {
        public void Configure(EntityTypeBuilder<BoardRole> builder)
        {
        }
    }
}