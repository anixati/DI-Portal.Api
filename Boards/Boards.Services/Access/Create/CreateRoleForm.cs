using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Access.Forms
{
    public class CreateRoleForm : FormBuilder
    {
        public override string FormName => $"create_approle";
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Role Details", AddDetails);
        }

        private void AddDetails(FormField field)
        {
            field.AddInput("Code", "Code", true);
            field.AddInput("Name", "Name", true);
            field.AddInput("Description", "Description", x =>
            {
                x.AddRequired("Description is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }
    }
}