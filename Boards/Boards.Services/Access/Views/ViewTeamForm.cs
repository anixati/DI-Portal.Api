using Boards.Services.Core;
using DI.Forms.Types;

namespace Boards.Services.Access.Views
{
    public class ViewTeamForm : BoardForms
    {
        public override string FormName => "view_appteam";

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Team Details", UserDetails);
            fs.AddSubGrid("Roles", "TeamRoleList", x => { x.AddAction("manage", "teamrole", "Manage Roles"); });
            fs.AddSubGrid("Users", "TeamUserList", x => { });
        }

        private void UserDetails(FormField field)
        {
            field.AddInput("Name", "Name", true);
            field.AddInput("Description", "Description", x =>
            {
                x.AddRequired("Description is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }
    }
}