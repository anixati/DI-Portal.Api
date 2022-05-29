using Di.Qry.Schema.Types;

namespace Boards.Services
{
    public static class Database
    {
        public static readonly TableKey Appointee = new("Appointee", "apt");
        public static readonly TableKey AppointeeView = new("AppointeesView", "avw");
    }
}