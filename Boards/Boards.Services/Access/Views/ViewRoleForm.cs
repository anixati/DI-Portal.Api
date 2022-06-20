using Boards.Services.Core;
using DI.Forms.Types;

namespace Boards.Services.Access.Views
{
    public class ViewRoleForm : BoardForms
    {
        public override string FormName => $"view_approle";
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Role Details", UserDetails);
            fs.AddSubgrid("Teams's", "RoleTeamList", x =>
            {
            });
            fs.AddSubgrid("Users's", "RoleUserList", x =>
            {
            });
        }

        private void UserDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("Name", "Name", true);
                f.AddInput("Code", "Code", true);
            });
            field.AddInput("Description", "Description", x =>
            {
                x.AddRequired("Description is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }
    }
}