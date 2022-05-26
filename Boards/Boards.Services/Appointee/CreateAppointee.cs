using DI.Forms.Builders;
using DI.Forms.Types;

namespace Boards.Services.Appointee
{
    public class CreateAppointee : FormBuilder
    {
        public override string FormName => "CreateAppointee";

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Personal Details", AddPersonDetails);
            fs.AddPage("Contact Details", AddContactDetails);
            fs.AddPage("Professional Details", AddProfessionalDetails);
        }

        private void AddPersonDetails(FormField field)
        {
            field.AddFieldGroup(FullName);
        }
        private void FullName(FormField field)
        {
            field.AddInput("Title",20);
            field.AddInput("FirstName",40);
            field.AddInput("LastName",40);

        }

        private void AddContactDetails(FormField field)
        {

        }
        private void AddProfessionalDetails(FormField field)
        {

        }
    }
}