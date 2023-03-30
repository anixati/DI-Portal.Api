using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Access.Create
{
    public class CreateTeamForm : FormBuilder
    {
        public override string FormName => "create_appteam";

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Team Details", AddDetails);
        }

        private void AddDetails(FormField field)
        {
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