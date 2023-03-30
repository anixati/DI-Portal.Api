using Boards.Services.Client;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Ministers.Lists
{
    public class InactiveList : QrySchema
    {
        public override string SchemaName => "InActiveMinisters";
        public override string Title => "InActive Ministers";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.MinistersView);
            pt.Column("FullName", "FullName", "Full Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.Minister.Path();
            });
            pt.AddSearchCol("FullName");
            pt.AddSearchCol("Gender");
            pt.AddSearchCol("Phone");
            pt.AddSearchCol("Mobile");
            pt.AddSearchCol("Fax");
            pt.AddSearchCol("City");
            pt.AddSearchCol("State");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "1");
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("FullName", false);
        }
    }
}