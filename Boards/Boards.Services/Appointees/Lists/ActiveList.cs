using Boards.Services.Core;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Appointees.Lists
{
    public class ActiveList : QrySchema
    {
        public override string SchemaName => "ActiveAppointees";
        public override string Title => "Active Appointees";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.AppointeeView);
            pt.Column("FullName", "FullName", "Full Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.Appointee.Path();
            });
            pt.AddSearchCol("FullName");
            pt.AddSearchCol("Gender");
            pt.AddSearchCol("City");
            pt.AddSearchCol("State");
            pt.AddSearchCol("Aboriginal");
            pt.AddSearchCol("Handicapped");
            pt.AddSearchCol("Regional");
            pt.AddSearchCol("Executive");
            pt.AddSearchCol("Capability");
            pt.AddSearchCol("Experience");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
        }


        protected override (string, bool) GetDefaultSort()
        {
            return ("FullName", false);
        }
    }
}