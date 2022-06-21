using Boards.Services.Core;
using DI.Forms.Types;

namespace Boards.Services.Access.Views
{
    public class ViewTeamForm : BoardForms
    {
        public override string FormName => $"view_appteam";
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Team Details", UserDetails);
            fs.AddSubgrid("Role's", "TeamRoleList", x =>
            {
                x.AddAction("manage", "teamrole", "Manage Role's");
            });
            fs.AddSubgrid("Users's", "TeamUserList", x =>
            {
            });
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