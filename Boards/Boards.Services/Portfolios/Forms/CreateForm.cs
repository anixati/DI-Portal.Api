using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Portfolios.Forms
{
    public class CreateForm : FormBuilder
    {
        public override string FormName => Constants.Forms.Portfolio.Create;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Portfolio Details", AddPersonDetails);
            //fs.AddPage("Minister Details", AddContactDetails);
           // fs.AddPage("Current Boards", AddContactDetails);
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

        private void AddContactDetails(FormField field)
        {
        }

        
    }
}