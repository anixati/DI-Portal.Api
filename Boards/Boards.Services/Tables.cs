using Di.Qry.Schema.Types;

namespace Boards.Services
{

    public static class Constants
    {
        public static class Forms
        {
            public static readonly FormKey Appointee = new FormKey("appointee");
        }

        public static class Db
        {
            public static readonly TableKey AppointeeView = new("AppointeesView", "avw");
        }
    }

}