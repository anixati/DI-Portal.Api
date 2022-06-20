using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Secretary.Lists
{
    public class LookupList : QrySchema
    {
        public override string SchemaName => "SecretaryLookup";
        public override string Title => "Current Secretaries";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.SecretaryView);
            pt.Column("FullName", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
            });
            pt.AddSearchCols("Gender", "State");
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