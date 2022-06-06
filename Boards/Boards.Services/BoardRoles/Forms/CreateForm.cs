using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.BoardRoles.Forms
{
    public class CreateForm : FormBuilder
    {
        public override string FormName => Constants.Forms.BoardRole.Create;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Role Details", AddDetails);
         
        }

        private void AddDetails(FormField field)
        {
            field.AddInput("Name", "Name", x =>
            {
                x.AddRequired("Name is required");
                x.AddRule(ValRule.Min(5, "Minimum 5 chars required"));
                x.FieldType = FormFieldType.Text;
            });

        }
    }
}