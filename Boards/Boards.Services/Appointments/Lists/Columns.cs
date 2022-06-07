using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Appointments.Lists
{

    public static class Columns
    {
        public static Table Default()
        {
            var pt = Table.Create(Constants.Db.AppointmentsView);
            pt.AddHiddenCols("BoardId", "BoardRoleId", "AppointeeId");
            pt.Column("AppointmentName", "Name", "Appointment", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "Id";
                x.LinkPath = "boards/appointments/";
            });
            pt.Column("BoardName", "BoardName", "Board", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "BoardId";
                x.LinkPath = "boards/list/";
            });

            pt.Column("RoleName", "RoleName", "Role", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "BoardRoleId";
                x.LinkPath = "boards/brdroles/";
            });
            pt.Column("AppointeeName", "AppointeeName", "Appointee", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "AppointeeId";
                x.LinkPath = "boards/appointees/";
            });
            pt.AddSearchCols("BriefNumber", "StartDate", "EndDate");

            return pt;
        }

    }
}