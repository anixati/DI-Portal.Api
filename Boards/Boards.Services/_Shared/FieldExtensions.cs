using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI;
using DI.Forms.Types;

namespace Boards.Services._Shared
{
    public static class FieldExtensions
    {

        public static void AddAddress(this FormField field, string prefix)
        {
            prefix.ThrowIfEmpty($"prefix is required");



            field.AddFieldGroup(f =>
            {
                f.AddInput($"{prefix}.Unit", "Unit/No.");
                f.AddInput($"{prefix}.Street", "Street Name");
                f.AddInput($"{prefix}.City", "Suburb/City");
            });
            field.AddFieldGroup(f =>
            {
                f.AddNumeric($"{prefix}.Postcode", "Postcode");
                f.AddInput($"{prefix}.State", "State");
                f.AddInput($"{prefix}.Country", "Country");
            });
        }
    }
}
