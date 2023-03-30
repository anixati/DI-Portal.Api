using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Boards.Lists
{
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
            qs.TeamId = "AppTeamId";
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }
}