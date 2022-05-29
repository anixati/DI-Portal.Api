using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Appointees
{
    public class CreateAppointee : FormBuilder
    {
        public override string FormName => "CreateAppointee";

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Personal Details", AddPersonDetails);
            fs.AddPage("Contact Details", AddContactDetails);
            fs.AddPage("Professional Details", AddProfessionalDetails);
            fs.AddPage("Other Details", OtherDetails);
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
            field.AddInput("Gender", "Gender", true);
        }

        private void AddContactDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("Email1", "Primary Email", true);
                f.AddInput("Email2", "other Email");
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("Mobile", "Mobile Number");
                f.AddInput("HomePhone", "Phone Number");
            });
            field.AddInput("FaxNumber", "Fax Number");
        }

        private void AddProfessionalDetails(FormField field)
        {
            field.AddInput("Biography", "Biography", x =>
            {
                x.AddRequired("Biography is required");
                x.AddRule(ValRule.Min(100, "Minimum 100 chars required"));
                x.FieldType = FormFieldType.Note;
            });
            field.AddInput("PostNominals", "PostNominals");
            field.AddFieldGroup(f =>
            {
                f.AddInput("ResumeLink", "Resume Link");
                f.AddInput("LinkedInProfile", "LinkedIn Profile");
            });
        }

        private void OtherDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsRegional", "Regional", "Please specify", true);
                f.AddYesNo("IsAboriginal", "Aboriginal", "Please specify", true);
            });

            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsDisabled", "Disabled", "Please specify");
                f.AddYesNo("ExecutiveSearch", "Executive", "Please specify");
            });
        }
    }
}