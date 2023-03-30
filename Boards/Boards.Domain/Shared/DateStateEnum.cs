using System.ComponentModel;

namespace Boards.Domain.Shared
{
    public enum DateStateEnum
    {
        NA = 0,
        TBA,
        Date
    }

    public enum YesNoOptionEnum
    {
        Yes = 0,
        No,
        UnableToDisclose
    }


    public enum FullTimeEnum
    {
        [Description("--")] NA = 0,
        [Description("Full time")] FullTime,
        [Description("Part time")] PartTime
    }
}