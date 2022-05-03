using System;
using System.Linq;
using System.Threading.Tasks;
using Boards.Infrastructure.Domain;
using DI.Domain.Options;
using DI.Domain.Seed;

namespace Boards.Infrastructure.Seeding
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
            var rng = new Random();
            var opr = GetRepo<OptionKey>();


            foreach (var jx in Enumerable.Range(1, rng.Next(30, 60)))
            {

                var op = await CreateIfNotExists(new OptionKey
                    {Name = $"Option Key {jx}", Code = $"OPCODE{jx}", Description = "well-crafted Git commit message is the best way to communicate context about a change to fellow developers" });
                await Store.SaveAsync();
                if (op != null)
                    foreach (var ix in Enumerable.Range(1, rng.Next(3, 5)))
                    {
                        var ov = Create(new OptionSet
                        {
                            OptionKeyId = op.Id,
                            Label = $"Random option {rng.Next(3000, 4000)}",
                            Value = rng.Next(1000, 8000),
                            Order = ix,
                            Description =
                                "Apple has discontinued macOS Server. Existing macOS Server customers can continue to download and use the app with macOS Monterey"
                        });
                    }
            }

            await Store.SaveAsync();

            //await CreateIfNotExists(new OptionKey
              //  {Name = "Share Options", Code = "SHAREOPTS", Description = "Share Options"});
        }
    }
}