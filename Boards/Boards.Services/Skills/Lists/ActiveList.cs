using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Skills.Lists
{
    public class ActiveList : QrySchema
    {
        public override string SchemaName => "ActiveSkills";
        public override string Title => "Active Skills";

        protected override Table CreateEntity()
        {
            var pt = Table.Create("SkillsView", "sv");
            pt.Column("Name", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "Id";
                x.LinkPath = Routes.Skills.Path();
            });
            pt.AddSearchCol("SkillType");
            pt.AddSearchCol("Description");
            pt.AddDateColumn("CreatedOn");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
        }


        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }
}