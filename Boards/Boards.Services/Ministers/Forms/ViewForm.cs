using Boards.Services._Shared;
using Boards.Services.Core;
using DI.Domain.Enums;
using DI.Forms.Types;

namespace Boards.Services.Ministers.Forms
{
    public class ViewForm : BoardForms
    {
        public override string FormName => Constants.Forms.Minister.View;

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Personal Details", AddPersonDetails);
            fs.AddSubGrid("Portfolios", "MinisterPortfolios", x => { });
            fs.AddDocGrid(Constants.Entities.Minister);
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
                f.AddPhone("HomePhone", "Phone", false);
                f.AddEmail("Email1", "Email", false);
            });
            field.AddDivider("Street Address");
            field.AddAddress("StreetAddress", false);
            field.AddDivider("Postal Address");
            field.AddAddress("PostalAddress", false);
        }
    }
}