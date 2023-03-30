using Boards.Services.Core;
using DI.Domain.Enums;
using DI.Forms.Types;

namespace Boards.Services.Users.Forms
{
    public class ViewForm : BoardForms
    {
        public override string FormName => Constants.Forms.User.View;

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Personal Details", AddPersonDetails);
            fs.AddSubGrid("Approved by Boards", "ApprovedUserBoards", x => { });
            fs.AddSubGrid("Resp. Officer Boards", "RespOfficerBoards", x => { });
        }

        private void AddPersonDetails(FormField field)
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
                f.AddEmail("Email1", "Email", false);
            });
            field.AddDivider();
        }
    }
}