using DI.Domain.Enums;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Ministers.Forms
{
    public class ViewForm : FormBuilder
    {
        public override string FormName => Constants.Forms.Minister.View;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Personal Details", AddPersonDetails);
            fs.AddTab("Address Details", AddAddressDetails);
           // fs.AddTab("Professional Details", AddProfessionalDetails);
           // fs.AddTab("Other Details", OtherDetails);
        }

        private void AddPersonDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("Title", "Title", false, 30);
                f.AddInput("FirstName", "First Name", true, 35);
                f.AddInput("LastName", "Last Name", true, 35);
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("MiddleName", "Middle Name", false, 30);
                f.AddSelect<GenderEnum>("Gender", "Gender", true, 30);

            });
            //field.AddDivider();

            field.AddFieldGroup(f =>
            {
                f.AddPhone("Mobile", "Mobile Number", false, 30);
                f.AddPhone("HomePhone", "Phone Number", false, 30);
                f.AddPhone("FaxNumber", "Fax Number", false, 30);
            });
            field.AddFieldGroup(f =>
         {
             f.AddEmail("Email1", "Primary Email", true, 30);
             f.AddEmail("Email2", "other Email", false, 30);
         });
        }

        private void AddAddressDetails(FormField field)
        {

        }

       
    }
}