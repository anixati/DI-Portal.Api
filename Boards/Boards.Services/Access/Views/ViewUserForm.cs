using Boards.Services.Client;
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
            fs.AddHeaders(f =>
            {
                f.AddLabel("UserId", "User Id");
                f.AddLabel("IsSystem", "System User");
            });
            fs.AddTab("User Details",UserDetails);
            fs.AddSubGrid("Role's", "UserRoleList", x =>
            {
                x.AddAction("manage", "userrole", "Manage Role's");
            });
            fs.AddSubGrid("Teams's", "UserTeamList", x =>
            {
                x.AddAction("manage", "teamuser", "Manage Teams's");
            });
        }
        private void UserDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("Title", "Title", false);
                f.AddInput("FirstName", "First Name", true);
                f.AddInput("MiddleName", "Middle Name", false);
                f.AddInput("LastName", "Last Name", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddSelect<GenderEnum>("Gender", "Gender", true);
                f.AddPhone("Mobile", "Mobile Number", false);
                f.AddPhone("HomePhone", "Phone Number", false);
                f.AddEmail("Email1", "Email", true);

            });
            field.AddDivider();
            field.AddFieldGroup(f =>
            {
                f.AddInput("Upn", "Upn", false, true);
                f.AddDate("AccessRequest", "Access Requested",false, true);
                f.AddDate("AccessGranted", "Access Granted", false, true);
                f.AddInput("DisplayName", "Display Name", false);
            });
        }
    }
}