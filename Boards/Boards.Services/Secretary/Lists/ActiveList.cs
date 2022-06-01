using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Secretary.Lists
{
    public class ActiveList : QrySchema
    {
        public override string SchemaName => "ActiveSecretaries";
        public override string Title => "Active Secretaries";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.SecretaryView);
            pt.Column("FullName", "FullName", "Full Name", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
            });
            pt.SearchCol("FullName");
            pt.SearchCol("Gender");
            pt.SearchCol("Phone");
            pt.SearchCol("Mobile");
            pt.SearchCol("Fax");
            pt.SearchCol("City");
            pt.SearchCol("State");
            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
        }


        protected override (string, bool) GetDefaultSort()
        {
            return ("FullName", false);
        }
    }
}