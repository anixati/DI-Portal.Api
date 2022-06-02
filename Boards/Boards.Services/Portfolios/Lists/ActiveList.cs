using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Portfolios.Lists
{
    public class ActiveList : QrySchema
    {
        public override string SchemaName => "ActivePortfolios";
        public override string Title => "Current Portfolios";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.PortFolioView);
            pt.Column("PortfolioName", "PortfolioName", "Portfolio Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
            });
            pt.SearchCol("PortfolioName");
            pt.SearchCol("Description");
            pt.SearchCol("CreatedOn");
            pt.SearchCol("Minister");
            pt.SearchCol("StartDate");
            pt.SearchCol("EndDate");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
        }


        protected override (string, bool) GetDefaultSort()
        {
            return ("PortfolioName", false);
        }
    }
}