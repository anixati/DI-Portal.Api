using System.Collections.Generic;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Access.Create
{
    public class ManageRolesForm : FormBuilder
    {
        public override string FormName => $"manage_userrole";
        protected override FormType FormType => FormType.MultiSelect;

        protected override void CreateSchema(FormSchema fs)
        {
            fs.Options = new List<SelectItem> {new("RolesList", "Select Role's")};
        }
    }
}