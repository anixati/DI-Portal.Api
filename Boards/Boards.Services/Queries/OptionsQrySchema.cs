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
            var ok = Entity.Create("OptionKeys", "ok");
            ok.Column("Name","GroupName");

            var link= ok.Join("OptionSet", "os", "OptionKeyId");
            link.Select("Id|OptionId", "Order","Label","Value");
            return ok;
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("Name",false);
        }
    }




}
