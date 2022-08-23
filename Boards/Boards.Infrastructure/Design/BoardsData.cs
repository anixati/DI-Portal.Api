using System;
using System.Linq;
using System.Threading.Tasks;
using Boards.Domain.Boards;
using Boards.Domain.Contacts;
using Boards.Infrastructure.Domain;
using DI.Domain;
using DI.Domain.Enums;
using DI.Domain.Options;
using DI.Domain.Owned;
using DI.Domain.Seed;
using DI.Domain.Users;
using DI.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;

namespace Boards.Infrastructure.Design
{
    public class BoardsData : DataSetupBase<BoardsDbContext>
    {
        private class MigUserIdentityProvider : IIdentityProvider
        {
            public IIdentity GetIdentity()
            {
                return new UserIdentity("", "SYSTEM", "");
            }
        }
        public BoardsData(DbDataStore<BoardsDbContext> context) : base(context)
        {
        }
        public static async Task Run(IServiceProvider serviceProvider)
        {
            try
            {
                var lf = serviceProvider.GetService<ILoggerFactory>();
                var options = serviceProvider.GetService<DbContextOptions<BoardsDbContext>>();
                var dbc = new BoardsDbContext(options, new MigUserIdentityProvider());
                using var context = new BoardsData(new DbDataStore<BoardsDbContext>(dbc,lf));
                await context.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAILED: {ex}");
            }
        }
        protected override async Task SetupDomainData()
        {
            //await CreateDummyOpSets();
            //await CreatePortfolios();
            //await CreateDummyAppointees();
           // await CreateDummyUsers();
           // await Save();
        }
        //private async Task CreateDummyOpSets()
        //{
        //    var rng = new Random();
        //    var osk = new string[]
        //    {
        //        "OwnerDivision", "BoardStatus", "Division", "OwnerPosition", "EstablishedByUnder",
        //        "Position", "Appointer", "RemunerationMethod", "MinSubLocation", "RemunerationPeriod",
        //        "AppointmentSource", "SelectionProcess", "Judicial","SkillType"

        //    };
        //    var repo = GetRepo<OptionKey>();
        //    var ix = 1;
        //    foreach (var jx in osk)
        //    {
        //        ix++;

        //        var op = await repo.FindAsync(x => EF.Functions.Like(x.Code, $"{jx.ToUpper()}"));
        //        if (op != null) continue;
        //        op = new OptionKey
        //        {
        //            Name = $"{jx}",
        //            Code = $"{jx.ToUpCase()}",
        //            Description = $"{jx} description"
        //        };
        //        await repo.CreateAsync(op);
        //        await Save();

        //        foreach (var i in Enumerable.Range(1, rng.Next(3, 12)))
        //        {
        //            var ov = Create(new OptionSet
        //            {
        //                OptionKeyId = op.Id,
        //                Label = $"Option {ix}{i}",
        //                Value = ix + i,
        //                Order = i,
        //                Description =
        //                    "Build more reliable software with AI companion"
        //            });
        //        }
        //    }
        //}
        //private async Task CreatePortfolios()
        //{
        //    var rng = new Random();
        //    var repo = GetRepo<Portfolio>();
        //    var ix = 1;
        //    foreach (var i in Enumerable.Range(1, rng.Next(6, 12)))
        //    {
        //        ix++;
        //        var jx = $"Portfolio {ix}";
        //        var op = await repo.FindAsync(x => EF.Functions.Like(x.Name, $"{jx}"));
        //        if (op != null) continue;
        //        op = new Portfolio
        //        {
        //            Name = $"{jx}",
        //            Description = $"{jx} description"
        //        };
        //        await repo.CreateAsync(op);
        //        await Save();
        //    }
        //}

        private async Task CreateDummyUsers()
        {

            var tmr = GetRepo<AppTeam>();
            var tur = GetRepo<TeamUser>();
            var team = await tmr.FindAsync(x => EF.Functions.Like(x.Name, "Default")); 
            var users = new string[]
            {
                "Admin","Super","View","Contrib"
            };
            var opr = GetRepo<AppUser>();
            foreach (var name in users)
            {
                var app = await opr.FindAsync(x => EF.Functions.Like(x.FirstName, name));
                if (app != null) continue;
                var ix = 1;
                var op = await opr.CreateAsync(new AppUser()
                {
                    UserId = name,
                    Title = "Mr",
                    FirstName = name,
                    LastName = $"User",
                    MiddleName = "",
                    HomePhone = "0262426242",
                    FaxNumber = "0236598956",
                    MobilePhone = "0401642369",
                    Email1 = $"{name}.User@gmail.com",
                    SecurityStamp = DateTime.UtcNow.ToString("o"),
                    PasswordHash = BC.HashPassword("Summer11"),
                    EmailConfirmed = true,
                    ChangePassword = true ,
                    StreetAddress = CreateAddress(),
                    PostalAddress = CreateAddress(),
                    Disabled = false,
                    Gender = ix % 2 == 0 ? GenderEnum.Male : GenderEnum.Female,
                });
                await Save();
                if (op != null)
                {
                    await tur.CreateAsync(new TeamUser {AppTeamId = team.Id, AppUserId = op.Id});
                }
                await Save();
                ix++;
            }





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

        
        }
    }