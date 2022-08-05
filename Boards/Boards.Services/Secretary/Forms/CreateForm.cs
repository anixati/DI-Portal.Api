using Boards.Services._Shared;
using DI.Domain.Enums;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Secretary.Forms
{
    public class CreateForm : FormBuilder
    {
        public override string FormName => Constants.Forms.Secretary.Create;

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Personal Details", AddPersonDetails);
            fs.AddPage("Address Details", AddAddressDetails);
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
            field.AddFieldGroup(f =>
            {
                f.AddEmail("Email1", "Email", true);
                f.AddPhone("HomePhone", "Phone");
            });
        }
        private void AddAddressDetails(FormField field)
        {
            field.AddDivider("Street Address");
            field.AddAddress("StreetAddress", true);
            field.AddDivider("Postal Address");
            field.AddAddress("PostalAddress", false);
        }

        
    }
}