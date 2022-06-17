using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Portfolios.Lists
{
    public class LookupList : QrySchema
    {
        public override string SchemaName => "PortfolioLookup";
        public override string Title => "Current Portfolios";

        protected override Table CreateEntity()
        {
            var pf = Table.Create(Constants.Db.Portfolio);
            pf.AddSearchCols("Name", "Description");
            return pf;
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