using Boards.Services.Core;
using DI.Domain.Enums;
using DI.Forms.Types;

namespace Boards.Services.Access.Views
{
    public class ViewUserForm : BoardForms
    {
        public override string FormName => $"view_appuser";
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("User Details",UserDetails);
            fs.AddSubgrid("Role's", "UserRoleList", x =>
            {
                x.AddAction("manage", "userrole", "Manage Role's");
            });
            fs.AddSubgrid("Teams's", "UserTeamList", x =>
            {
                x.AddAction("manage", "teamuser", "Manage Teams's");
            });
        }

        private void UserDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("Title", "Title", false, 30);
                f.AddInput("FirstName", "First Name", true, 30);
                f.AddInput("LastName", "Last Name", true, 30);
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("MiddleName", "Middle Name", false, 30);
                f.AddSelect<GenderEnum>("Gender", "Gender", true, 30);

            });
            field.AddFieldGroup(f =>
            {
                f.AddPhone("Mobile", "Mobile Number", false, 30);
                f.AddPhone("HomePhone", "Phone Number", false, 30);
                f.AddPhone("FaxNumber", "Fax Number", false, 30);
            });
            field.AddFieldGroup(f =>
            {
                f.AddEmail("Email1", "Primary Email", true, 30);
                f.AddEmail("Email2", "Other Email", false, 30);
            });
        }
    }
}