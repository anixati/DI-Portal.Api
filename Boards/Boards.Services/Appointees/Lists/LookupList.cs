using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Appointees.Lists
{
    public class LookupList : QrySchema
    {
        public override string SchemaName => "AppointeeLookup";
        public override string Title => "Current Appointees";

        protected override Table CreateEntity()
        {
            var pf = Table.Create(Constants.Db.AppointeeView);
            pf.Column("FullName", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
            });
            pf.AddSearchCols("Gender");
            return pf;
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