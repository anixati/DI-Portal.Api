using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Boards.Lists
{
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
            qs.TeamId = "AppTeamId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }
}