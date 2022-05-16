using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Di.Qry;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Queries
{
    public class OptionsQrySchema : QrySchema
    {
        public override string SchemaName => "OptionSets";

        protected override Entity CreateEntity()
        {
            return new Entity("OptionKeys", "oks")
                .AddCols("Name", "Description")
                .AddSortCol("Name");
        }
    }




}
