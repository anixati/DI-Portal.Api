using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Appointments.Lists
{
    public class RoleAppointmentsList : QrySchema
    {
        public override string SchemaName => "RoleAppointmentsView";
        public override string Title => "Role Appointments";
        protected override Table CreateEntity()
        {
            return Shared.Default();
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "BoardRoleId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }
}