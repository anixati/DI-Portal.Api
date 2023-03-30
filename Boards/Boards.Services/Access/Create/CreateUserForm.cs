using DI.Domain.Enums;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Access.Create
{
    public class CreateUserForm : FormBuilder
    {
        public override string FormName => "create_appuser";

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Login Details", LoginDetails);
            fs.AddPage("User Details", AddDetails);
        }

        private void LoginDetails(FormField field)
        {
            field.AddFieldGroup(f => { f.AddInput("UserId", "User Id"); });
        }

        private void AddDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("Title", "Title");
                f.AddInput("FirstName", "First Name", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("MiddleName", "Middle Name");
                f.AddInput("LastName", "Last Name", true);
            });
            field.AddSelect<GenderEnum>("Gender", "Gender", false);
            field.AddFieldGroup(f =>
            {
                f.AddEmail("Email1", "Primary Email", false);
                f.AddPhone("HomePhone", "Phone Number");
            });
        }
    }
}