using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Appointee
{
    public class ActiveAppointeeList : QrySchema
    {
        public override string SchemaName => "ActiveAppointees";
        public override string Title => "Active Appointees";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Database.AppointeeView);
            pt.SearchCol("FullName");
            pt.SearchCol("Gender");
            pt.SearchCol("Phone");
            pt.SearchCol("Mobile");
            pt.SearchCol("Fax");
            pt.SearchCol("City");
            pt.SearchCol("State");
            pt.SearchCol("IsAboriginal");
            pt.SearchCol("IsDisabled");
            pt.SearchCol("IsRegional");
            pt.SearchCol("ExecutiveSearch");
            pt.SearchCol("Capability");
            pt.SearchCol("Experience");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled","=", "0");
        }


        protected override (string, bool) GetDefaultSort()
        {
            return ("FullName", false);
        }
    }

    public class InActiveAppointeeList : QrySchema
    {
        public override string SchemaName => "InActiveAppointees";
        public override string Title => "InActive Appointees";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Database.AppointeeView);
            pt.SearchCol("FullName");
            pt.SearchCol("Gender");
            pt.SearchCol("Phone");
            pt.SearchCol("Mobile");
            pt.SearchCol("Fax");
            pt.SearchCol("City");
            pt.SearchCol("State");
            pt.SearchCol("IsAboriginal");
            pt.SearchCol("IsDisabled");
            pt.SearchCol("IsRegional");
            pt.SearchCol("ExecutiveSearch");
            pt.SearchCol("Capability");
            pt.SearchCol("Experience");
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