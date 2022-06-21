using System.Collections.Generic;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Appointees.Forms
{
    public class ManageSkillsForm : FormBuilder
    {
        public override string FormName => $"manage_appointeeskill";
        protected override FormType FormType => FormType.MultiSelect;

        protected override void CreateSchema(FormSchema fs)
        {
            fs.Options = new List<SelectItem> { new("ActiveSkills", "Select Skill's") };
        }
    }
}