using Di.Qry.Schema.Types;

namespace Boards.Services
{
    public static class Constants
    {
        public static class Entities
        {
            public static readonly string Board = "Board";
            public static readonly string Appointee = "Appointee";
            public static readonly string BoardAppointment = "BoardAppointment";
            public static readonly string BoardRole = "BoardRole";
            public static readonly string Minister = "Minister";
            public static readonly string Portfolio = "Portfolio";
            public static readonly string AssistantSecretary = "AssistantSecretary";
        }

        public static class Forms
        {
            public static readonly FormKey Boards = new("board");
            public static readonly FormKey BoardRole = new("boardrole");
            public static readonly FormKey BoardAppointment = new("boardappointment");

            public static readonly FormKey Appointee = new("appointee");
            public static readonly FormKey Minister = new("minister");
            public static readonly FormKey Secretary = new("secretary");
            public static readonly FormKey Portfolio = new("portfolio");
            public static readonly FormKey Skill = new("skill");

            public static readonly FormKey MinisterTerm = new("ministerterm");
            public static readonly FormKey User = new("user");


            public static readonly FormKey AppUser = new("appuser");
            public static readonly FormKey AppRole = new("approle");
            public static readonly FormKey AppTeam = new("appteam");
        }

        public static class Db
        {
            public static readonly TableKey Portfolio = new("Portfolios", "pf");

            public static readonly TableKey AppointeeView = new("AppointeesView", "avw");
            public static readonly TableKey MinistersView = new("MinistersView", "mvw");
            public static readonly TableKey SecretaryView = new("SecretariesView", "svw");
            public static readonly TableKey PortFolioView = new("PortfoliosView", "pvw");
            public static readonly TableKey BoardsView = new("ActiveBoardsView", "bvw");
            public static readonly TableKey BoardRolesView = new("BoardRolesView", "bvw");
            public static readonly TableKey AppointmentsView = new("AppointmentsView", "bvw");

            public static readonly TableKey MinisterTermsView = new("MinisterTermsView", "mts");

            public static readonly TableKey UsersView = new("ActiveUsersView", "uvw");
        }
    }
}