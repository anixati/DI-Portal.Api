using Boards.Services.Core;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Portfolios.Forms
{
    public class ViewForm : BoardForms
    {
        public override string FormName => Constants.Forms.Portfolio.View;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Portfolio Details", AddPersonDetails);
            fs.AddTab("Minister Details", AddAddressDetails);
            fs.AddTab("Current Boards", AddAddressDetails);
        }

        private void AddPersonDetails(FormField field)
        {
            field.AddInput("Name", "Name", x =>
            {
                x.AddRequired("Name is required");
                x.AddRule(ValRule.Min(5, "Minimum 5 chars required"));
                x.FieldType = FormFieldType.Text;
            });
            field.AddInput("Description", "Description", x =>
            {
                x.AddRequired("Description is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }

        private void AddAddressDetails(FormField field)
        {

        }

       
    }
}