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
            });
            pt.SearchCol("FullName");
            pt.SearchCol("Gender");
            //pt.SearchCol("Phone");
            //pt.SearchCol("Mobile");
            //pt.SearchCol("Fax");
            pt.SearchCol("City");
            pt.SearchCol("State");
            pt.SearchCol("Aboriginal");
            pt.SearchCol("Handicapped");
            pt.SearchCol("Regional");
            pt.SearchCol("Executive");
            pt.SearchCol("Capability");
            pt.SearchCol("Experience");
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