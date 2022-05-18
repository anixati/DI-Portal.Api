using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Queries
{
    public class OptionsQrySchema : QrySchema
    {
        public override string SchemaName => "OptionSets";

        protected override Entity CreateEntity()
        {
            var ok = Entity.Create("OptionKeys", "ok");
            ok.AddCol("Name","GroupName");

            var os = Entity.Create("OptionSet", "os");
            os.AddCols("Id|OptionId", "Order","Label","Value");

            ok.Join(os, "OptionKeyId", "Id");
            return ok;
        }
    }




}
