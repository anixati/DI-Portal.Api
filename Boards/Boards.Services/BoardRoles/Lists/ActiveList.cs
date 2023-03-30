using Boards.Services.Client;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.BoardRoles.Lists
{
    public class ActiveList : QrySchema
    {
        public override string SchemaName => "BoardRolesView";
        public override string Title => "Active Roles";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.BoardRolesView);
            pt.AddHiddenCols("BoardId", "IncumbentId");
            pt.Column("Name", "Name", "Position", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.Roles.Path();
            });
            pt.Column("Incumbent", "Incumbent", "Incumbent", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "IncumbentId";
                x.LinkPath = Routes.Appointee.Path();
                ;
            });
            pt.AddSearchCols("IncumbentGender", "Appointer", "ActingInRole", "ExcludeGenderBalance");


            pt.AddDateColumn("StartDate");
            pt.AddDateColumn("EndDate");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "BoardId";
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }

    public class InActiveList : ActiveList
    {
        public override string SchemaName => "BoardInactiveRolesView";
        public override string Title => "Inactive Roles";

  
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "1");
            qs.ParentId = "BoardId";
        }

    }
}