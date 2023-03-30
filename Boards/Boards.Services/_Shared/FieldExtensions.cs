using DI;
using DI.Forms.Types;

namespace Boards.Services._Shared
{
    public static class FieldExtensions
    {
        public static void AddAddress(this FormField field, string prefix, bool required)
        {
            prefix.ThrowIfEmpty("prefix is required");


            field.AddFieldGroup(f =>
            {
                f.AddInput($"{prefix}.Unit", "Unit/No.", false);
                f.AddInput($"{prefix}.Street", "Street Name", false);
                f.AddInput($"{prefix}.City", "Suburb/City", false);
            });
            field.AddFieldGroup(f =>
            {
                f.AddNumeric($"{prefix}.Postcode", "Postcode", false);
                f.AddTextList($"{prefix}.State", "States", "State", required);
                f.AddTextList($"{prefix}.Country", "Countries", "Country", false, "Australia");
            });
        }
    }
}