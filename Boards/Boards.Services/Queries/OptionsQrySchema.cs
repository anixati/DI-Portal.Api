using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Queries
{
    public class OptionsQrySchema : QrySchema
    {
        public override string SchemaName => "OptionSets";
        public override string Title => "Vacant Pos";

        protected override Table CreateEntity()
        {
            var ok = Table.Create("OptionKeys", "ok");
            ok.SearchCol("Name", "GroupName");

            var link = ok.Join("OptionSet", "os", "OptionKeyId");
            link.Select("Id|OptionId", "Order", "Value");
            link.SelectSearchCols("Label");
            return ok;
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }
}