using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Skills.Lists
{
    public class AppointeeSkills : QrySchema
    {
        public override string SchemaName => "AppointeeSkills";
        public override string Title => "Skills";
        protected override Table CreateEntity()
        {
            var pt = Table.Create("AppointeeSkillsView","asv");
            pt.AddHiddenCols( "SkillId", "AppointeeId");
            pt.Column("Skill", "Skill", "Skill", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.LinkId = "SkillId";
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.Skills.Path();
            });
            pt.AddSearchCol("SkillType");
            return pt;
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "AppointeeId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Skill", false);
        }
    }
}