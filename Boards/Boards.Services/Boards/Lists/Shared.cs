﻿using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Boards.Lists
{
    public static class Shared
    {
        public static Table BoardViewTable()
        {
            var pt = Table.Create(Constants.Db.BoardsView);
            pt.AddHiddenCols("PortfolioId", "RespOfficerId", "ApprovedUserId", "AsstSecretaryId");

            pt.Column("Name", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkPath = Routes.Boards.Path();
            });
            pt.AddSearchCol("Acronym");
            pt.Column("Portfolio", "Portfolio", "Portfolio", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "PortfolioId";
                x.LinkPath = Routes.Portfolios.Path();
            });
            pt.AddSearchCol("OwnerDivision");
            pt.AddSearchCol("OwnerPosition");
            pt.Column("RespOfficer", "RespOfficer", "Resp Officer", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "RespOfficerId";
                x.LinkPath = "boards/portfolios/";
                x.LinkPath = Routes.Users.Path();
            });
            pt.Column("AsstSecretary", "AsstSecretary", "Asst. Secretary", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "AsstSecretaryId";
                x.LinkPath = Routes.Secretary.Path();
            });
            return pt;
        }
    }
}