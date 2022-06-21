using Boards.Services.Core;
using DI.Forms.Types;

namespace Boards.Services.Skills.Forms
{
    public class ViewForm : BoardForms
    {
        public override string FormName => Constants.Forms.Skill.View;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Skill Details", AddPersonDetails);
            fs.AddSubgrid("Appointees", "SkillAppointee", x =>
            {

            });

        }

        private void AddPersonDetails(FormField field)
        {
            field.AddInput("Name", "Name", x =>
            {
                x.AddRequired("Name is required");
                x.AddRule(ValRule.Min(5, "Minimum 5 chars required"));
                x.FieldType = FormFieldType.Text;
            });
            field.AddPickList("SkillType", "SkillType", "Skill Type", true);
            field.AddInput("Description", "Description", x =>
            {
                x.AddRequired("Description is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }
    }
}