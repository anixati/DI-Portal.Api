using Boards.Services.Client;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Skills.Lists
{
    public class SkillAppointee : QrySchema
    {
        public override string SchemaName => "SkillAppointee";
        public override string Title => "Appointees";
        protected override Table CreateEntity()
        {
            var pt = Table.Create("AppointeeSkillsView", "asv");
            pt.AddHiddenCols("SkillId", "AppointeeId");
            pt.Column("FullName", "FullName", "FullName", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "AppointeeId";
                x.LinkPath = Routes.Appointee.Path();
            });
            pt.AddSearchCol("SkillType");
            return pt;
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "SkillId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("FullName", false);
        }
    }
}