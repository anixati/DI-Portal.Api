using System.ComponentModel;

namespace DI.Domain.Enums
{
    public enum GenderEnum
    {
        [Description(" -- ")]
        Na = 0,
        Male,
        Female,
        Indeterminate,
        [Description("Chose not to say")]
        ChoseNotToSay
    }
}