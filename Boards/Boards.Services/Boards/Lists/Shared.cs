using Boards.Services.Client;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Boards.Lists
{
    public static class Shared
    {
        public static Table BoardViewTable()
        {
            var pt = Table.Create(Constants.Db.BoardsView);
            pt.AddHiddenCols("PortfolioId", "RespOfficerId", "ApprovedUserId", "AsstSecretaryId", "AppTeamId");

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
            pt.AddSearchCols("OwnerDivision|Division", "OwnerPosition|Position");
            pt.Column("RespOfficer", "RespOfficer", "Resp Officer", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "RespOfficerId";
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
            pt.AddSearchCol("Approved");
            return pt;
        }
    }
}