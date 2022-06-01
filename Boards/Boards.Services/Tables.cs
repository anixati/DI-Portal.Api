using Di.Qry.Schema.Types;

namespace Boards.Services
{

    public static class Constants
    {
        public static class Forms
        {
            public static readonly FormKey Appointee = new FormKey("appointee");
            public static readonly FormKey Minister = new FormKey("minister");
            public static readonly FormKey Secretary = new FormKey("secretary");
        }

        public static class Db
        {
            public static readonly TableKey AppointeeView = new("AppointeesView", "avw");
            public static readonly TableKey MinistersView = new("MinistersView", "avw");
            public static readonly TableKey SecretaryView = new("SecretariesView", "avw");
        }
    }

}