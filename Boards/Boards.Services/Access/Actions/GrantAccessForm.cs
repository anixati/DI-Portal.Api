using Boards.Domain;
using Boards.Services.Core;
using DI.Domain.Core;
using DI.Domain.Users;
using DI.Exceptions;
using DI.Forms.Handlers;
using DI.Forms.Requests;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DI;
using DI.Security.Core;

namespace Boards.Services.Access.Actions
{

    public class GrantAcccessModel : IViewModel
    {
        public long Id { get; set; }
        public string Comments { get; set; }
    }

    public class GrantAccessForm : DialogBuilder
    {
        public override string FormName => $"grant_access";
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Grant User Access", UserDetails);
        }

        private void UserDetails(FormField field)
        {
            field.AddInput("Comments", "Comments", x =>
            {
                x.FieldType = FormFieldType.Note;
            });
        }
    }

    public class GrantAccessDialog : BoardsDialogHandler<GrantAcccessModel>
    {
        public GrantAccessDialog(ILoggerFactory logFactory, IBoardsContext boardsContext) : base(logFactory, boardsContext)
        {
        }

        public override string SchemaKey => $"grant_access";

        protected override async Task OnExecute(GrantAcccessModel model, IDictionary<string, object> data, long entityId)
        {
            var repo = GetRepo<AppUser>();
            var user = await repo.GetById(entityId);
            if (user == null) throw new BuisnessException($"User not found!");
            if (user.AccessGranted.HasValue) throw new BuisnessException($"User already granted access!");
            user.AccessGranted = DateTime.Now;
            await repo.UpdateAsync(user);
            await SaveAsync();

            //-- assign default team 
            var team = await GetRepo<AppTeam>().FindAsync(x => x.Name == "Default");
            team.ThrowIfNull($"Default team not found");
            var teammUser= await GetRepo<TeamUser>().FindAsync(x => x.AppTeamId == team.Id && x.AppUserId == user.Id);
            if(teammUser == null)
                teammUser = await GetRepo<TeamUser>().CreateAsync(new TeamUser { AppTeamId = team.Id, AppUserId = user.Id });
            await SaveAsync();

            //assign defualt role 
            var role = await GetRepo<AppRole>().FindAsync(x => x.Name == $"{ApplicationRoles.Contributor}");
            role.ThrowIfNull($"contributer role not found");
            var userRole = await GetRepo<UserRole>().FindAsync(x => x.AppRoleId == role.Id && x.AppUserId == user.Id);
            if (userRole == null)
                userRole = await GetRepo<UserRole>().CreateAsync(new UserRole { AppRoleId = role.Id, AppUserId = user.Id });
            await SaveAsync();
            
        }

    }

}
