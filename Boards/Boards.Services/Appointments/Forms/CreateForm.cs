using Boards.Services.Client;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Appointments.Forms
{
    public class CreateForm : FormBuilder
    {
        public override string FormName => Constants.Forms.BoardAppointment.Create;

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Appointment Details", AddDetails);
            fs.AddPage("Remuneration Details", TypeDetails);
        }

        private void AddDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddLookup("Appointee", "AppointeeLookup", Routes.Appointee, "Appointees", true);
                f.AddInput("BriefNumber", "Brief Number", false);
                f.AddPickList("Appointer", "Appointer", "Appointer/Approver", false);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("Proposed", "Proposed Appointment", "", false);
                f.AddDate("InitialStartDate", "Initial Start Date - Firts Appointed", false);
                f.AddDate("StartDate", "Start Date", false);
            });
            field.AddFieldGroup(f =>
            {
                f.AddDate("AppointmentDate", "Appointment Date (Instrument signed on)", false);
                f.AddYesNo("EndDateUnknown", "End Date Unknown", "", false);
                f.AddDate("EndDate", "End Date", false);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("ActingInRole", "Acting in the role", "", false);
                f.AddNumeric("PrevTerms", "How many terms previously served?", false);
            });
        }

        private void TypeDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddDecimal("AnnumAmount", "Per-annum Amount", false);
                f.AddPickList("RemunerationPeriod", "RemunerationPeriod", "Remuneration Period", false);
                f.AddNumeric("PrevTerms", "Previous Terms", false);
            });
            field.AddFieldGroup(f =>
            {
                f.AddPickList("AppointmentSource", "AppointmentSource", "Appointment Source", false);
                f.AddPickList("SelectionProcess", "SelectionProcess", "Selection Process", false);
                f.AddPickList("Judicial", "Judicial", "Judicial", false);
            });

            field.AddYesNo("ExclGenderReport", "Exclude from Gender Balance Report", "", false);
        }
    }
}