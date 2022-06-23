using Boards.Services.Client;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Terms.Lists
{

    public class PortfolioMinistersList : QrySchema
    {
        public override string SchemaName => "PortfolioMinisters";
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
                x.LinkPath = Routes.Minister.Path();
            });

            pf.Column("Portfolio", "Portfolio", "Portfolio", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "PortfolioId";
                x.LinkPath = Routes.Portfolios.Path();
            });
            pf.AddSearchCols("StartDate", "EndDate");
            return pf;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "PortfolioId";
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("StartDate", false);
        }
    }
}