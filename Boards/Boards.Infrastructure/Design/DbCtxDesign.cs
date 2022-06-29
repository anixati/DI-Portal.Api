using Boards.Infrastructure.Domain;
using DI.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Boards.Infrastructure.Design
{
    public class DbCtxDesign : IDesignTimeDbContextFactory<BoardsDbContext>
    {
        private class MigUserIdentityProvider : IIdentityProvider
        {
            public IIdentity GetIdentity()
            {
                return new UserIdentity("SYSTEM", "","");
            }
        }
        public BoardsDbContext CreateDbContext(string[] args)
        {
            var dbCtxType = typeof(BoardsDbContext);
            var ctxAssembly = dbCtxType.Assembly;
            var connStr = "Server=XD401462;Database=DI_Boards;Trusted_Connection=True;Pooling=true;";
            // var connStr = "Server=DESKTOP-ANJVLKR\\SQLEXPRESS;Database=DI_Boards;Trusted_Connection=True;Pooling=true;";
            var dbOptions = new DbContextOptionsBuilder<BoardsDbContext>()
                .UseSqlServer(connStr, ox => { ox.MigrationsAssembly(ctxAssembly.FullName); })
                .Options;

            return new BoardsDbContext(dbOptions, new MigUserIdentityProvider());
        }
    }
}