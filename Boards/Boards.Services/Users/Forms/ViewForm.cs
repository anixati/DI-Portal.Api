using Boards.Services.Core;
using DI.Domain.Enums;
using DI.Forms.Types;

namespace Boards.Services.Users.Forms
{
    public class ViewForm : BoardForms
    {
        public override string FormName => Constants.Forms.AppUser.View;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Personal Details", AddPersonDetails);
            fs.AddSubGrid("Approved by Boards's", "ApprovedUserBoards", x =>
            {
            });
            fs.AddSubGrid("Resp. Officer Boards's", "RespOfficerBoards", x =>
            {
            });
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
    }
}