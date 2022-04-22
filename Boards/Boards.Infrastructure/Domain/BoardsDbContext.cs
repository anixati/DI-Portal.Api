using System.Linq;
using Boards.Domain.Boards;
using Boards.Domain.Roles;
using DI.Domain.Services;
using DI.Security;
using Microsoft.EntityFrameworkCore;

namespace Boards.Infrastructure.Domain
{
    public class BoardsDbContext : DomainDbBase<BoardsDbContext>
    {
        public BoardsDbContext()
        {
        }

        public BoardsDbContext(DbContextOptions<BoardsDbContext> options, IIdentityProvider provider)
            : base(options, provider)
        {
        }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<BoardRoleEvent> RoleEvents { get; set; }
        public DbSet<MinisterPortfolio> MinisterEvents { get; set; }


        protected override void ConfigureModels(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict; //disable cascade deletes
            }

        }
    }
}