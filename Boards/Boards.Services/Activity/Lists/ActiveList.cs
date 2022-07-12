using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boards.Services.Boards.Lists;
using Boards.Services.Client;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Documents.Lists
{
    public class ActiveList : QrySchema
    {
        public override string SchemaName => "Documents";
        public override string Title => "Documents";
        protected override Table CreateEntity()
        {
            var tbl = Table.Create("Activities", "act");

            tbl.Column("Title", "Title", "Title", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                //x.LinkPath = Routes.Default;
            });
            tbl.AddSearchCols("ModifiedOn", "ModifiedBy");



            tbl.AddQryField("EntityName");
            tbl.AddQryField("EntityId");
            return tbl;
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.Where("Deleted", "=", "0");
            // qs.TeamId = "AppTeamId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }


    }
}
