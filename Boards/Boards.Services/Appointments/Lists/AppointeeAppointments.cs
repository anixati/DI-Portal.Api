using Boards.Services.Client;
using Di.Qry.Core;
using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Appointments.Lists
{
    public class AppointeeAppointments : QrySchema
    {
        public override string SchemaName => "AppointeeAppointments";
        public override string Title => "Active Appointments";

        protected override Table CreateEntity()
        {
            var pt = Table.Create(Constants.Db.AppointmentsView);
            pt.AddHiddenCols("BoardId", "BoardRoleId", "AppointeeId");
            pt.Column("AppointmentName", "Name", "Appointment", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "Id";
                x.LinkPath = Routes.Appointment.Path();
            });
            pt.Column("BoardName", "BoardName", "Board", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "BoardId";
                x.LinkPath = Routes.Boards.Path();
            });

            pt.Column("RoleName", "RoleName", "Role", x =>
            {
                x.Searchable = true;
                x.Sortable = true;
                x.Type = ColumnType.HyperLink;
                x.LinkId = "BoardRoleId";
                x.LinkPath = Routes.Roles.Path();
            });
            //pt.Column("AppointeeName", "AppointeeName", "Appointee", x =>
            //{
            //    x.Searchable = true;
            //    x.Sortable = true;
            //    x.Type = ColumnType.HyperLink;
            //    x.LinkId = "AppointeeId";
            //    x.LinkPath = Routes.Appointee.Path();
            //});

            pt.AddSearchCols("BriefNumber");

            pt.AddSearchCols("ActingInRole");

            pt.AddDateColumn("StartDate");
            pt.AddDateColumn("EndDate");
            pt.AddDateColumn("AppointmentDate");

            return pt;
        }

        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "AppointeeId";
        }

        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }



    public class AppointeeInactiveAppointments : AppointeeAppointments
    {
        public override string SchemaName => "AppointeeInactiveAppointments";
        public override string Title => "Inactive Appointments";

     
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "1");
            qs.ParentId = "AppointeeId";
        }

      
    }
}