using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Ministers.Lists
{
    public class LookupList : QrySchema
    {
        public override string SchemaName => "MinisterLookup";
        public override string Title => "Current Ministers";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.MinistersView);
            pt.Column("FullName", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
            });
            pt.AddSearchCol("Gender");
            pt.AddSearchCol("City");
            pt.AddSearchCol("State");
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