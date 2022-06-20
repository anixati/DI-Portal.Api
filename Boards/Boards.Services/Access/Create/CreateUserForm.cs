using DI.Domain.Enums;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Access.Forms
{
    public class CreateUserForm : FormBuilder
    {
        public override string FormName => $"create_appuser";
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Login Details", AddLoginDetails);
            fs.AddPage("User Details", AddDetails);
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
            field.AddSelect<GenderEnum>("Gender", "Gender", true);
            field.AddFieldGroup(f =>
            {
                f.AddEmail("Email1", "Primary Email", true);
                f.AddPhone("HomePhone", "Phone Number");
            });
        }
        private void AddLoginDetails(FormField field)
        {
            field.AddInput("UserId", "UserId",true,80);
        }

    }
}