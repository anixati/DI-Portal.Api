using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Users.Lists
{
    public class InactiveList : QrySchema
    {
        public override string SchemaName => "InActiveUsers";
        public override string Title => "Inactive Users";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.UsersView);
            pt.Column("FullName", "Name", "Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
            });
            pt.AddSearchCols("Phone", "Email", "CreatedOn");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "1");
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("FullName", false);
        }
    }
}