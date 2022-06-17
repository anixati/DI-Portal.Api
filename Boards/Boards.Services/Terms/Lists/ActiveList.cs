using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Terms.Lists
{

    public class ActiveList : QrySchema
    {
        public override string SchemaName => "MinisterTermsView";
        public override string Title => "Minister Terms";

        protected override Table CreateEntity()
        {
            var pf = Table.Create(Constants.Db.MinisterTermsView);
            pf.AddHiddenCols("MinisterId", "PortfolioId");

            pf.Column("Minister", "Minister", "Minister", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "MinisterId";
                x.LinkPath = "boards/ministers/";
            });
           
            pf.Column("Portfolio", "Portfolio", "Portfolio", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "PortfolioId";
                x.LinkPath = "boards/portfolios/";
            });
            pf.AddSearchCols("StartDate", "EndDate");
            return pf;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("StartDate", false);
        }
    }
}