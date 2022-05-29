using System;
using System.Linq;
using System.Threading.Tasks;
using Boards.Domain.Contacts;
using Boards.Infrastructure.Domain;
using DI.Domain.Enums;
using DI.Domain.Options;
using DI.Domain.Owned;
using DI.Domain.Seed;
using Microsoft.EntityFrameworkCore;

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
                Console.WriteLine($"FAILED: {ex}");
                // throw new Exception($"Failed to setup - {ex}");
            }
        }


        protected override async Task SetupDomainData()
        {
            await CreateDummyAppointees();

            await Store.SaveAsync();

            //await CreateIfNotExists(new OptionKey
            //  {Name = "Share Options", Code = "SHAREOPTS", Description = "Share Options"});
        }

        private async Task CreateDummyAppointees()
        {
            var rng = new Random();
            var opr = GetRepo<Appointee>();
            foreach (var jx in Enumerable.Range(1, rng.Next(5, 10)))
            {
                var name = $"First{jx}";


                var app = await opr.FindAsync(x => EF.Functions.Like(x.FirstName, name));
                if (app != null) continue;

                var op = await opr.CreateAsync(new Appointee
                {
                    Title = "Mr",
                    FirstName = $"First{jx}",
                    LastName = $"Last{jx}",
                    MiddleName = "Sake",
                    HomePhone = "0262426242",
                    FaxNumber = "0236598956",
                    MobilePhone = "0401642369",
                    Email1 = $"teste{jx}@gmail.com",
                    Email2 = $"teste{jx * 6}@gmail.com",
                    StreetAddress = CreateAddress(),
                    PostalAddress = CreateAddress(),
                    Biography =
                        "well-crafted Git commit message is the best way to communicate context about a change to fellow developers",
                    ResumeLink = "https://google.com",
                    Disabled = jx % 2 == 0 ? true : false,
                    Gender = jx % 2 == 0 ? GenderEnum.Male : GenderEnum.Female,
                    IsAboriginal = jx % 2 == 0 ? true : false,
                    IsDisabled = jx % 2 == 0 ? true : false,
                    IsRegional = jx % 2 == 0 ? true : false,
                    ExecutiveSearch = jx % 2 == 0 ? true : false
                });
            }
        }

        private AddressType CreateAddress()
        {
            var rng = new Random();
            return new AddressType
            {
                Unit = $"{rng.Next(200, 300)}",
                Street = $"Road no {rng.Next(200, 300)}",
                City = "Canberra",
                Postcode = 2148,
                State = "ACT",
                Country = "Australia"
            };
        }


        private async Task CreateDummyOpSets()
        {
            var rng = new Random();
            var opr = GetRepo<OptionKey>();
            foreach (var jx in Enumerable.Range(1, rng.Next(30, 60)))
            {
                var op = await CreateIfNotExists(new OptionKey
                {
                    Name = $"Option Key {jx}", Code = $"OPCODE{jx}",
                    Description =
                        "well-crafted Git commit message is the best way to communicate context about a change to fellow developers"
                });
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
        }
    }
}