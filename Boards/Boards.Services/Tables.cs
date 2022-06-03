using Di.Qry.Schema.Types;

namespace Boards.Services
{

    public static class Constants
    {
        public static class Forms
        {

            public static readonly FormKey Boards = new FormKey("board");
            public static readonly FormKey BoardRole = new FormKey("boardrole");
            public static readonly FormKey BoardAppointment = new FormKey("boardappointment");

            public static readonly FormKey Appointee = new FormKey("appointee");
            public static readonly FormKey Minister = new FormKey("minister");
            public static readonly FormKey Secretary = new FormKey("secretary");
            public static readonly FormKey Portfolio = new FormKey("portfolio");
        }

        public static class Db
        {
            public static readonly TableKey Portfolio = new("Portfolios", "pf");

            public static readonly TableKey AppointeeView = new("AppointeesView", "avw");
            public static readonly TableKey MinistersView = new("MinistersView", "mvw");
            public static readonly TableKey SecretaryView = new("SecretariesView", "svw");
            public static readonly TableKey PortFolioView = new("PortfoliosView", "pvw");
            public static readonly TableKey BoardsView = new("ActiveBoardsView", "bvw");
            public static readonly TableKey UsersView = new("UsersView", "uvw");
        }
    }

}