using Di.Qry.Schema.Types;

namespace Boards.Services
{
    public static class Database
    {
        public static readonly TableKey Appointee = new TableKey("Appointee","apt");
        public static readonly TableKey AppointeeView = new TableKey("AppointeesView", "avw");

    }


}