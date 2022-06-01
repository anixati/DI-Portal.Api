using DI.Domain.Enums;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Ministers.Forms
{
    public class CreateForm : FormBuilder
    {
        public override string FormName => Constants.Forms.Minister.Create;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Personal Details", AddPersonDetails);
            fs.AddPage("Contact Details", AddContactDetails);
            //fs.AddPage("Professional Details", AddProfessionalDetails);
            //fs.AddPage("Other Details", OtherDetails);
        }

        private void AddPersonDetails(FormField field)
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
        }

        private void AddContactDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddEmail("Email1", "Primary Email", true);
                f.AddEmail("Email2", "other Email");
            });
            field.AddFieldGroup(f =>
            {
                f.AddPhone("Mobile", "Mobile Number");
                f.AddPhone("HomePhone", "Phone Number");
            });
            field.AddPhone("FaxNumber", "Fax Number");
        }

        
    }
}