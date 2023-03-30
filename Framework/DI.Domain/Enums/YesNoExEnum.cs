using System.ComponentModel;

namespace DI.Domain.Enums
{
    public enum YesNoExEnum
    {
        [Description(" -- ")] Na = 0,
        Yes,
        No,
        [Description("Chose not to say")] ChoseNotToSay
    }

    public enum RegionalEnum
    {
        [Description(" -- ")] Na = 0,
        Regional,
        Metro,
        [Description("Chose not to say")] ChoseNotToSay
    }
}