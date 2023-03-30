using Di.Qry.Schema;

namespace Boards.Services.Skills.Lists
{
    public class InActiveList : ActiveList
    {
        public override string SchemaName => "InactiveSkills";
        public override string Title => "Inactive Skills";

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "1");
        }
    }
}