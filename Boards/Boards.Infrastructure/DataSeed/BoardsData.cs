using Boards.Infrastructure.Domain;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Seed;
using DI.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boards.Infrastructure.DataSeed
{
    public class BoardsData : DataSetupBase<BoardsDbContext>
    {
        public BoardsData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public static async Task Run(IServiceProvider serviceProvider)
        {
            try
            {
                using var context = new BoardsData(serviceProvider);
                await context.Run();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to setup - {ex}");
            }
        }

       
        
        protected override async Task SetupDomainData()
        {
            try
            {
                var rng = new Random();
                await Store.BeginTransaction();

                var opr = GetRepo<OptionKey>();
                var op = await CreateIfNotExists( new OptionKey { Name = "Selection Process",Code ="SELPROCESS", Description = "Selection Process" });
                if (op != null)
                {
                    foreach(var ix in Enumerable.Range(1, rng.Next(2, 55)))
                    {
                        var ov = new OptionSet
                        {
                            OptionKeyId= op.Id,
                            Label = $"Random option {rng.Next(3000, 4000)}",
                            Value = rng.Next(1000, 8000),
                            Order= ix,
                            Description = "Apple has discontinued macOS Server. Existing macOS Server customers can continue to download and use the app with macOS Monterey"
                        };
                    }

                }
                await Store.SaveAsync();
                await CreateIfNotExists( new OptionKey { Name = "Share Options", Code = "SHAREOPTS", Description = "Share Options" });




                await Store.SaveAsync();
                Store.Commit();
            }
            catch (Exception e)
            {
                Store.Rollback();
                Console.WriteLine($"{e}");
                throw;
            }

        }
    }
}