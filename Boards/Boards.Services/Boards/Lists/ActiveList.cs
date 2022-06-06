using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Boards.Lists
{
    public class ActiveList : QrySchema
    {
        public override string SchemaName => "ActiveBoards";
        public override string Title => "Active Boards";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.BoardsView);
            pt.AddHiddenCols("PortfolioId", "RespOfficerId", "ApprovedUserId", "AsstSecretaryId");
            pt.SearchCol("Acronym");
            pt.Column("Name", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
            });
            pt.Column("Portfolio", "Portfolio", "Portfolio", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "PortfolioId";
                x.LinkPath = "boards/portfolios/";
            });
            pt.SearchCol("OwnerDivision");
            pt.SearchCol("OwnerPosition");
            pt.Column("RespOfficer", "RespOfficer", "Resp Officer", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "RespOfficerId";
            });
            pt.Column("AsstSecretary", "AsstSecretary", "Asst. Secretary", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "AsstSecretaryId";
            });
            return pt;
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