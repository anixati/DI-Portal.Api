﻿using Boards.Services.Client;
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
                x.LinkPath = Routes.Portfolios.Path();
            });
            pt.AddSearchCol("PortfolioName");
            pt.AddSearchCol("Description");
            pt.AddDateColumn("CreatedOn");
            // pt.AddSearchCol("Minister");
            //pt.AddSearchCol("StartDate");
            //pt.AddSearchCol("EndDate");
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