using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Boards.Domain.Boards;
using Boards.Domain.Contacts;
using Boards.Domain.Roles;
using Boards.Domain.Shared;
using Boards.Infrastructure.Domain;
using DataTools.Migrations;
using DI.Domain.Core;
using DI.Domain.Enums;
using DI.Domain.Options;
using DI.Domain.Owned;
using DI.Domain.Services;
using DI.Domain.Users;
using DI.Security.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BC = BCrypt.Net.BCrypt;

namespace DataTools
{
    public class ImportCrmDataService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        protected readonly IDataStore<BoardsDbContext> _db;
        public ImportCrmDataService(ILoggerFactory logFactory, IConfiguration configuration, IDataStore<BoardsDbContext> db)
        {
            _logger = logFactory.CreateLogger(GetType().Name);
            _config = configuration;
            _db = db;
        }

        public void Trace(string msg)
        {
            _logger.LogInformation($"  {msg}");
        }
        protected IRepository<TK> GetRepo<TK>() where TK : class, IEntity
        {
            return _db.Repo<TK>();
        }
        protected async Task Save()
        {
            await _db.SaveAsync();
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var fileName = _config["Import:FileName"];
                Trace($"Importing Boards Crm Data from  {fileName}");

                if (!File.Exists(fileName))
                    throw new Exception($"file not found - {fileName}");
                await _db.BeginTransaction();


                var dataStr = await File.ReadAllTextAsync(fileName, cancellationToken);
                var data = JsonConvert.DeserializeObject<BoardsData>(dataStr);
                await AddAppData(data);
                await ImportOptions(data);
                await ImportPortfolios(data);
                await ImportUsers(data);
                await ImportContacts(data);

                await ImportBoards(data);

                await ImportBoardRoles(data);
              
                await ImportBoardAppointments(data);
                _db.Commit();
                Trace($"!---Completed---!");
            }
            catch (Exception ex)
            {
                _db.Rollback();
                _logger.LogError(ex, "Error!");
            }

        }
        protected async Task<TK> CreateIfNotExists<TK>(TK entity) where TK : class, INamedEntity
        {
            var repo = _db.Repo<TK>();
            var op = await repo.FindAsync(x => EF.Functions.Like(x.Name, entity.Name)) ??
                     await repo.CreateAsync(entity);
            return op;
        }

        private async Task AddAppData(BoardsData data)
        {

            var roleRepo = GetRepo<AppRole>();
            foreach (ApplicationRoles role in Enum.GetValues(typeof(ApplicationRoles)))
            {
                var entity = await CreateIfNotExists(new AppRole
                {
                    Name = $"{role}",
                    Code = $"{(int)role}",
                    Description = role.ToDesc(),
                    Locked = true,
                    IsSystem = true
                });
            }
            await Save();
            var team = await CreateIfNotExists(new AppTeam
            {
                Name = $"Default",
                Description = "Default Team",
                Locked = true,
                IsSystem = true
            });
            await Save();



            var users = new string[]
            {
               "Super"
            };
            var usrRepo = GetRepo<AppUser>();
            var tuRepo = GetRepo<TeamUser>();
            foreach (var name in users)
            {
                var ausr = await usrRepo.FindAsync(x => EF.Functions.Like(x.FirstName, name));
                if (ausr != null) continue;
                ausr = new AppUser()
                {
                    UserId = name,
                    DisplayName="Administrator",
                    Title = "Mr",
                    FirstName = name,
                    LastName = $"User",
                    MiddleName = "",
                    HomePhone = "0262426242",
                    FaxNumber = "0236598956",
                    MobilePhone = "0401642369",
                    Email1 = $"{name}.User@gmail.com",
                    PasswordHash = BC.HashPassword("Summer11"),
                  AccessRequest = DateTime.Now,
                  AccessGranted = DateTime.Now,
                    Disabled = false,
                    IsSystem = true 
                };
                await usrRepo.CreateAsync(ausr);
                await Save();
                await tuRepo.CreateAsync(new TeamUser { AppTeamId = team.Id, AppUserId = ausr.Id });
                await Save();
            }

            var ruRepo = GetRepo<UserRole>();
            var spUser = await usrRepo.FindAsync(x => EF.Functions.Like(x.FirstName, "Super"));
            var suRole = await roleRepo.FindAsync(x => EF.Functions.Like(x.Name, $"{ApplicationRoles.SysAdmin}"));
            var suUserRole = await ruRepo.FindAsync(x => x.AppUserId == spUser.Id && x.AppRoleId==suRole.Id);
            if (suUserRole == null)
            {
                await ruRepo.CreateAsync(new UserRole { AppRoleId = suRole.Id, AppUserId = spUser.Id });
                await Save();
            }

        }

        private async Task ImportPortfolios(BoardsData data)
        {
            Trace($"Importing Board portfolios {data.Portfolios.Count}");
            var repo = GetRepo<Portfolio>();
            foreach (var (key, value) in data.Portfolios)
            {
                var id = value.Get("doca_portfolioid");
                var op = await repo.FindAsync(x => x.MigratedId == id);
                if (op != null) continue;
                Trace($"Creating {id}");

                op = new Portfolio()
                {
                    Name = value.Get("doca_name"),
                    Description =  value.Get("doca_name"),
                    MigratedId = id
                };
                op.Disabled = value.IsDisabled();
                await repo.CreateAsync(op);
                await Save();


                var team = await CreateIfNotExists(new AppTeam
                {
                    Name = $"{op.Name} Team",
                    Description = $"{op.Name} Team",
                    Locked = true,
                    IsSystem = true
                });
                await Save();

            }
            
            Trace($"Importing Board ministers {data.Ministers.Count}");
            var mrepo = GetRepo<Minister>();
            var mtrepo = GetRepo<MinisterTerm>();

            foreach (var (key, value) in data.Ministers)
            {
                var id = value.Get("doca_ministerid");
                var op = await mrepo.FindAsync(x => x.MigratedId == id);
                if (op != null) continue;
                Trace($"Creating {id}");
                var names = value.Get("doca_name").Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries);
                op = new Minister()
                {
                    FirstName = names[0],
                    LastName = names[1],
                    MigratedId = id
                };
                op.Disabled = value.IsDisabled();
                op = await mrepo.CreateAsync(op);
                await Save();

                var rid = value.Get("doca_portfolio");
                if (!string.IsNullOrEmpty(rid))
                {
                    var pf = await repo.FindAsync(x => x.MigratedId == rid.GetRefId());
                    if (pf != null)
                    {

                        var mt = await mtrepo.FindAsync(x => x.PortfolioId == pf.Id && x.MinisterId == op.Id);
                        if (mt == null)
                        {
                            mt = new MinisterTerm() {MinisterId = op.Id, PortfolioId = pf.Id};
                            await mtrepo.CreateAsync(mt);
                            await Save();
                        }
                    }
                }
                await Save();
            }


        }


        private async Task ImportBoardAppointments(BoardsData data)
        {


            Trace($"Importing Board Appointments {data.Appointments.Count}");
            var repo = GetRepo<BoardAppointment>();
            foreach (var (key, value) in data.Appointments)
            {

                var id = value.Get("new_appointmentid");
                var op = await repo.FindAsync(x => x.MigratedId == id);
                if (op != null) continue;
                Trace($"Creating {id}");

                var boardId = value.Get("new_roleboard");

                long bid = 0;
                var rid = value.Get("new_roleappointmentid");
                if(string.IsNullOrEmpty(rid)) continue;

                if (string.IsNullOrEmpty(boardId))
                {
                    bid = await GetBoardIdByRoleId(rid.GetRefId());
                }
                else
                {
                     bid = await GetBoardId(boardId.GetRefId());
                }

             

                var brid = await GetBoardRoleId(rid.GetRefId());
                var apeid = await GetContactId(value.Get("new_contactappointmentid").GetRefId());

                var aprid = await GetOption(value.Get("new_appointedby"), "Appointer");
                var asid = await GetOption(value.Get("new_appointmentsourceprocess"), "AppointmentSource");
                var jdid = await GetOption(value.Get("new_judicial"), "Judicial");
                var rpid = await GetOption(value.Get("new_remunerationperiod"), "RemunerationPeriod");
                var spid = await GetOption(value.Get("new_selectionprocess"), "SelectionProcess");

                // var pid = await GetOption(value.Get("new_type"), "Source");
                // var pid = await GetOption(value.Get("new_type"), "SelectionProcess");

                var rem = value.Get("new_remuneration").ToDeci();

                var name = value.Get("new_name");
                if (string.IsNullOrEmpty(name))
                {
                    name = "N/A";
                }

                op = new BoardAppointment()
                {
                    Name = name,
                    BoardId = bid,
                    BoardRoleId = brid,
                    AppointeeId = apeid,
                    JudicialId = jdid,
                    SelectionProcessId = spid,
                    RemunerationPeriodId = rpid.GetValueOrDefault(),
                    AppointmentSourceId = asid,
                    MigratedId = id,
                    StartDate = value.Get("new_startdate").ToDate().GetValueOrDefault(),
                    EndDate = value.Get("new_enddate").ToDate(),
                    BriefNumber = value.Get("new_briefnumber"),
                    IsCurrent = value.Get("new_currentappointment") == "true",
                    IsExOfficio = value.Get("new_enddate") == "",
                    IsFullTime = value.Get("new_fulltimeparttime") == "true",
                    ActingInRole = value.Get("new_substantiveacting") == "true",
                    ExclGenderReport = value.Get("new_genderreportable") == "true",
                    AnnumAmount = rem.GetValueOrDefault(),
                    AppointmentDate = value.Get("new_appointmentdate").ToDate(),
                    InitialStartDate = value.Get("new_initialstartdatefirstappointed").ToDate(),
                    PrevTerms = value.Get("new_numberoftermsserved").ToInt(),
                    IsSemiDiscretionary = value.Get("new_semidiscretionary")=="true",
                    Proposed = value.Get("new_proposed")=="true",
                    AppointerId = aprid,

                };
                op.Disabled = value.IsDisabled();
                await repo.CreateAsync(op);
                await Save();
            }

        }

        private async Task<long> GetBoardIdByRoleId(string roleId)
        {
            var repo = GetRepo<BoardRole>();
            var et = await repo.FindAsync(x => x.MigratedId == roleId);
            return et.BoardId;
        }

        private async Task ImportBoardRoles(BoardsData data)
        {


            Trace($"Importing Board roles {data.Roles.Count}");
            var repo = GetRepo<BoardRole>();
            foreach (var (key, value) in data.Roles)
            {
                var id = value.Get("new_roleid");
                var op = await repo.FindAsync(x => x.MigratedId == id);
                if (op != null) continue;
                Trace($"Creating {id}");

                var pid = await GetOption(value.Get("new_type"), "Position");
                var bid = await GetBoardId(value.Get("new_boardroleid").GetRefId());
                var apbyid = await GetOption(value.Get("new_appointedby"), "RoleAppointer");

                var rmid = await GetOption(value.Get("new_howisremunerationset"), "RemunerationMethod");
                var mlid = await GetOption(value.Get("new_minsublocation"), "MinSubLocation");


                var rem = value.Get("new_remuneration").ToDeci();

                var pdmsNo = value.Get("new_minsub");
                var remTbnl = value.Get("new_remunerationtribunaldetermination");

                var ftEnum = value.Get("new_fulltimeparttime") == "true"
                    ? FullTimeEnum.FullTime
                    : FullTimeEnum.PartTime;

                op = new BoardRole()
                {
                    Name = value.Get("new_name"),
                    BoardId = bid,
                    PositionId = pid.GetValueOrDefault(),
                    RoleAppointerId = apbyid.GetValueOrDefault(),
                    MigratedId = id,

                    IsFullTime = ftEnum,
                    IsExecutive = value.Get("new_execnonexec") == "Executive",
                    IsExOfficio = value.Get("new_exofficio") == "true",
                    IsApsEmployee = value.Get("new_apsemployee") == "true",
                    IsExNominated = value.Get("new_semidiscretionary") == "true",
                    Term = value.Get("new_term").ToInt(),

                    PositionRemunerated = value.Get("new_positionremunerated") == "Yes" ? YesNoOptionEnum.Yes : YesNoOptionEnum.No,
                    PaAmount = rem.GetValueOrDefault(),
                    RemunerationMethodId = rmid.GetValueOrDefault(),

                    RemunerationTribunal = string.IsNullOrEmpty(remTbnl) ? "N/A" : remTbnl,
                    VacantFromDate = value.Get("new_datevacantsince").ToDate(),

                    ExcludeFromOrder15 = value.Get("new_excludefromsenateorder15report") == "true" ? true : false,
                    ExcludeGenderReport = value.Get("new_genderreportable") == "true" ? true : false,
                    IsSignAppointment = value.Get("new_significantappointment") == "true" ? true : false,
                    //Excludedreason =  value.Get("new_reasonpositionnotgenderreportable"),
                    NextSteps = value.Get("new_nextsteps"),


                    //-------- Ministerial-----------

                    InstrumentLink = value.Get(""),
                    PDMSNumber = string.IsNullOrEmpty(pdmsNo) ? "N/A" : pdmsNo,
                    MinSubLocationId = mlid.GetValueOrDefault(),
                    MinisterOfficeDate = value.Get("new_minsubdate").ToDate(),
                    MinisterActionDate = value.Get("new_ministerialdecisionby").ToDate(),



                    LetterToPmDateType = value.Get("new_minlettertopm").ToDateState(),
                    LetterToPmDate = value.Get("new_minlettertopmdate").ToDate(),

                    ExCoDateType = value.Get("new_exco").ToDateState(),
                    ExCoDate = value.Get("new_excodate").ToDate(),

                    NotifyLetterDateType = value.Get("new_notificationletters").ToDateState(),
                    NotifyLetterDate = value.Get("new_notificationlettersdate").ToDate(),

                    CabinetDateType = value.Get("new_cabinet").ToDateState(),
                    CabinetDate = value.Get("new_cabinetdate").ToDate(),

                    InternalNotes = value.Get("new_responsibilities"),


                    ProcessStatus = value.Get(""),
                    LeadTimeToAppoint = value.Get("new_leadtimetoappoint").ToInt(),

                    //MinSubDateType = value.Get(""),
                    //MinSubDate = value.Get(""),


                };
                op.Disabled = value.IsDisabled();


                await repo.CreateAsync(op);
                await Save();
            }
        }


        private async Task<long?> GetContactId(string id)
        {
            var repo = GetRepo<Appointee>();
            var et = await repo.FindAsync(x => x.MigratedId == id);
            return et?.Id;
        }
        private async Task<long> GetBoardRoleId(string id)
        {
            var repo = GetRepo<BoardRole>();
            var et = await repo.FindAsync(x => x.MigratedId == id);
            return et.Id;
        }
        private async Task<long> GetBoardId(string id)
        {
            var repo = GetRepo<Board>();
            var et = await repo.FindAsync(x => x.MigratedId == id);
            return et.Id;
        }
        private async Task<long?> GetUserId(string id)
        {
            var repo = GetRepo<AppUser>();
            var et = await repo.FindAsync(x => x.MigratedId == id);
            return et?.Id;
        }

        private async Task<long?> GetOption(string label, string name)
        {
            var repo = GetRepo<OptionKey>();
            var et = await repo.FindAsync(x => x.Name == name);
            if (et == null) throw new Exception($"unknown option key");


            var osr = GetRepo<OptionSet>();

            if (string.IsNullOrEmpty(label))
            {
                var dfo = await osr.GetListAsync(x => x.OptionKeyId == et.Id);
                return dfo.First().Id;
            }

            var osv = await osr.FindAsync(x => x.OptionKeyId == et.Id && x.Label == label);
            if (osv != null)
            {
                return osv.Id;
            }

            return null;
        }

        private async Task<long?> GetSec(string label)
        {
            if (string.IsNullOrEmpty(label)) return null;
            var rx = label.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var fn = rx[0];
            var ln = "";
            if (rx.Length > 0) ln = rx[1];


            var repo = GetRepo<AssistantSecretary>();
            var et = await repo.FindAsync(x => x.FirstName == fn && x.LastName == ln);
            if (et == null)
            {
                et = new AssistantSecretary()
                {
                    FirstName = fn,
                    LastName = ln

                };
                await repo.CreateAsync(et);
                await Save();

            }
            return et.Id;
        }


        private async Task ImportBoards(BoardsData data)
        {

            var pfr = GetRepo<Portfolio>();
           
            var atr = GetRepo<AppTeam>();
            var atm = await atr.FindAsync(x => x.Name == "Default");
            if (atm == null)
            {
                atm = new AppTeam()
                {
                    Name = "Default",
                    Description = "Default Team"
                };
                await atr.CreateAsync(atm);
                await Save();
            }


            Trace($"Importing Boards {data.Boards.Count}");
            var repo = GetRepo<Board>();
            foreach (var (key, value) in data.Boards)
            {
                var id = value.Get("new_boardid");
                var op = await repo.FindAsync(x => x.MigratedId == id);
                if (op != null) continue;
                Trace($"Creating {id}");

                var v1 = await GetOption(value.Get("new_ownerdivisionoption"), "OwnerDivision");
                var v2 = await GetOption(value.Get("new_ownerpositionoptions"), "OwnerPosition");
                var v3 = await GetOption(value.Get("new_boardactivestatus"), "BoardStatus");
                var v4 = await GetOption(value.Get("new_establishedbyunder"), "EstablishedByUnder");


                var pf = await pfr.FindAsync(x => EF.Functions.Like(x.Name,"Arts"));
                var pfref = value.Get("doca_portfolio");
                if (!string.IsNullOrEmpty(pfref))
                {
                    var pfid = pfref.GetRefId();
                    pf = await pfr.FindAsync(x => x.MigratedId == pfid);
                }

                op = new Board()
                {
                    PortfolioId = pf.Id,
                    AppTeamId = atm.Id,
                    Name = value.Get("new_name"),
                    Description = value.Get("new_description").Replace("\\r\\n", " "),
                    Summary = value.Get("new_actionpending"),
                    EstablishedByUnderText = value.Get("new_establishedbyundermoreinfo"),
                    NominationCommittee = value.Get("new_nominationcommittee"),
                    LegislationReference = value.Get("new_legislationreference"),
                    Constitution = value.Get("new_constitution"),
                    QuorumRequiredText = value.Get("new_quorumrequiredtext"),
                    MigratedId = id,



                    MaximumTerms = value.Get("new_maximumterms").ToInt(),
                    OptimumMembers = value.Get("new_optimummembers").ToInt(),
                    MaximumMembers = value.Get("new_maximummembers").ToInt(),
                    MinimumMembers = value.Get("new_minimummembers").ToInt(),
                    QuorumRequired = value.Get("new_quorumrequired").ToInt().GetValueOrDefault(),
                    MaxServicePeriod = value.Get("new_maximumperiodofservice").ToInt(),




                    ReportingApproved = value.Get("new_approvalcheck") == "true" ? true : false,
                    ApprovedUserId = await GetUserId(value.Get("new_approvedby").GetRefId()),
                    ResponsibleUserId = await GetUserId(value.Get("new_responsibleofficeruser").GetRefId()),

                    OwnerDivisionId = v1.GetValueOrDefault(),
                    OwnerPositionId = v2.GetValueOrDefault(),
                    BoardStatusId = v3.GetValueOrDefault(),
                    EstablishedByUnderId = v4.GetValueOrDefault(),

                    AsstSecretaryPhone = value.Get("new_responsibleasphone"),
                };

                var secId = await GetSec(value.Get("new_responsibleas"));
                if (secId != null)
                    op.AsstSecretaryId = secId.GetValueOrDefault();

                op.Disabled = value.IsDisabled();
                await repo.CreateAsync(op);
                await Save();

            }

        }

        private async Task ImportContacts(BoardsData data)
        {
            Trace($"Importing Appointees {data.Contacts.Count}");
            var repo = GetRepo<Appointee>();
            foreach (var (key, value) in data.Contacts)
            {
                var id = value.Get("contactid");
                var fn = value.Get("firstname");
                var ln = value.Get("lastname");
                var name = value.Get("fullname");
                if (string.IsNullOrEmpty(fn))
                {
                    var names = name.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    fn = names[0];
                    ln = names.Length >= 1 ? names[1] : value.Get("lastname");
                }

                if (string.IsNullOrEmpty(ln)) continue;
                
                var op = await repo.FindAsync(x => x.MigratedId == id);
                if (op != null) continue;
                Trace($"Creating {fn} {ln} {value.Get("territorycode")}");

                var gender = value.Get("gendercode") == "Female" ? GenderEnum.Female : value.Get("gendercode") == "Male" ? GenderEnum.Male : GenderEnum.Na;

                var at = new AddressType()
                {
                    Street = value.Get("address1_line1").Replace("\\r\\n", " "),
                    City = value.Get("address1_city"),
                    Postcode = 0,
                    State = value.Get("territorycode"),
                    Country = "Australia"
                };

                op = new Appointee()
                {
                    Title = value.Get("new_title"),
                    FirstName = $"{fn}",
                    LastName = ln,
                    MiddleName = value.Get("middlename"),
                    MigratedId = value.Get("contactid"),
                    Gender = gender,
                    Email1 = value.Get("emailaddress1"),
                    Biography = value.Get("new_biography").Replace("\\r\\n", " "),
                    PostNominals = value.Get("new_postnomials"),
                    ResumeLink = value.Get("new_resumelink"),
                    LinkedInProfile = value.Get("new_linkedinprofile"),
                    MobilePhone = value.Get("telephone1").Limit(10),
                    HomePhone = value.Get("mobilephone").Limit(10),
                    //= value.Get(""),
                    StreetAddress = at,
                    IsRegional = value.Get("new_regionalmetro") != "Metro",
                    IsAboriginal = value.Get("new_culturalbackground") != "No",
                    IsDisabled = value.Get("new_disabilitystatus") != "No",
                    ExecutiveSearch = value.Get("new_identificationmethod") == "Executive Search",
                    //FaxNumber=value.Get("new_personalinterestdec")
                    //birthdate = value.Get("birthdate"),
                    //doca_type = value.Get("doca_type"),
                    //jobtitle = value.Get("jobtitle"),
                };
                op.Disabled = value.IsDisabled();
                await repo.CreateAsync(op);
                await Save();

            }


        }

        private async Task ImportUsers(BoardsData data)
        {
            Trace($"Importing Users {data.Users.Count}");
            var repo = GetRepo<AppUser>();
            foreach (var (key, value) in data.Users)
            {

                var usn = value.Get("domainname");
                if (string.IsNullOrEmpty(usn)) continue;
                var fn = value.Get("firstname");
                if (string.IsNullOrEmpty(fn)) continue;
                var ln = value.Get("lastname");
                if (string.IsNullOrEmpty(ln)) continue;
                var op = await repo.FindAsync(x => x.UserId == usn);
                if (op != null) continue;
                Trace($"Creating {fn} {ln}");
                op = new AppUser()
                {
                    FirstName = $"{fn}",
                    LastName = ln,
                    MiddleName = value.Get("middlename"),
                    MigratedId = value.Get("systemuserid"),
                    UserId = $"{usn}",
                    PasswordHash = "-",
                    AccessRequest = null,
                    AccessGranted = null
                };
                var email = value.Get("internalemailaddress");
                if (!string.IsNullOrEmpty(email))
                    op.Email1 = email;

                var isd = value.Get("isdisabled");
                if (!string.IsNullOrEmpty(isd) && isd == "true")
                {
                    op.Disabled = true;
                }
                else
                {
                    op.Disabled = false;
                }
                await repo.CreateAsync(op);
                await Save();

            }

        }


        private async Task ImportOptions(BoardsData data)
        {
            Trace($"Importing Options {data.Options.Count}");
            var okr = GetRepo<OptionKey>();
            var osr = GetRepo<OptionSet>();

            var ix = 1;
            foreach (var (key, value) in data.Options)
            {
                ix++;

                var op = await okr.FindAsync(x => EF.Functions.Like(x.Code, $"{key.ToUpper()}"));
                if (op == null)
                {
                    Trace($"Creating {key}");
                    op = new OptionKey
                    {
                        Name = $"{key}",
                        Code = $"{key.ToUpCase()}",
                        Description = $"{key} description"
                    };
                    await okr.CreateAsync(op);
                    await Save();
                }

                var od = 0;
                foreach (var (s, value1) in value)
                {
                    od++;
                    var ov = await osr.FindAsync(x => x.OptionKeyId == op.Id && x.Label == s);
                    if (ov == null)
                    {
                        Trace($"    {s} for {op.Id}");
                        ov = new OptionSet
                        {
                            OptionKeyId = op.Id,
                            Label = $"{s}",
                            Value = int.Parse(value1),
                            Order = od,
                            Description = $"{s}"
                        };
                        await osr.CreateAsync(ov);
                        await Save();
                    }
                }
            }

            Trace($"Options key count {await osr.CountAsync()}");
            Trace($"Options set count {await okr.CountAsync()}");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Trace($"Done");
            await Task.Delay(0, cancellationToken);
        }
    }
}