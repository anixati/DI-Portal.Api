﻿using Di.Qry.Schema;
using Di.Qry.Schema.Types;

namespace Boards.Services.Appointments.Lists
{
    public class BoardAppointmentsList : QrySchema
    {
        public override string SchemaName => "BoardAppointmentsView";
        public override string Title => "Board Appointments";
        protected override Table CreateEntity()
        {
            return Columns.Default();
        }
        protected override void ConfigureQry(QryState qs)
        {
            qs.Where("Disabled", "=", "0");
            qs.ParentId = "BoardId";
        }
        protected override (string, bool) GetDefaultSort()
        {
            return ("Name", false);
        }
    }
}