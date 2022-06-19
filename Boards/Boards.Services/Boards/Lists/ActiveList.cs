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
            pt.AddHiddenCols("PortfolioId", "RespOfficerId", "ApprovedUserId", "AsstSecretaryId");

            pt.Column("Name", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
            });
            pt.SearchCol("Acronym");
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
    }


    public class ActiveList : QrySchema
    {
        public override string SchemaName => "ActiveBoards";
        public override string Title => "Active Boards";
        protected override Table CreateEntity()
        {
            return Shared.BoardViewTable();
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

    public class InactiveList : QrySchema
    {
        public override string SchemaName => "InActiveMinisters";
        public override string Title => "InActive Ministers";
        protected override Table CreateEntity()
        {
            return Shared.BoardViewTable();
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "1");
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }

    public class RespOfficerBoards : QrySchema
    {
        public override string SchemaName => "RespOfficerBoards";
        public override string Title => "Active Boards";
        protected override Table CreateEntity()
        {
            return Shared.BoardViewTable();
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "RespOfficerId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }


    public class SecretaryBoards : QrySchema
    {
        public override string SchemaName => "SecretaryBoards";
        public override string Title => "Active Boards";
        protected override Table CreateEntity()
        {
            return Shared.BoardViewTable();
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "AsstSecretaryId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }


    public class ApprovedUserBoards : QrySchema
    {
        public override string SchemaName => "ApprovedUserBoards";
        public override string Title => "Active Boards";
        protected override Table CreateEntity()
        {
            return Shared.BoardViewTable();
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "ApprovedUserId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }

    public class PortfolioBoards : QrySchema
    {
        public override string SchemaName => "PortfolioBoards";
        public override string Title => "Active Boards";
        protected override Table CreateEntity()
        {
            return Shared.BoardViewTable();
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "PortfolioId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }
}