using Boards.Domain.Shared;
using Boards.Services.Client;
using Boards.Services.Core;
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
                f.AddInput("BriefNumber", "Brief Number", true);
            });
            
            field.AddFieldGroup(f =>
            {
                f.AddPickList("Appointer", "Appointer", "Appointer/Approver", true);
                f.AddDate("StartDate", "Start Date", true);
                f.AddDate("EndDate", "End Date", false);

            });
            field.AddFieldGroup(f =>
            {
                f.AddDate("AppointmentDate", "Appointment Date", true);
                f.AddDate("InitialStartDate", "Initial Start Date", false);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsExOfficio", "Is ExOfficio", "", true);
                f.AddYesNo("IsFullTime", "Full Time", "", false);
                f.AddYesNo("ActingInRole", "Acting", "", false);
            });

            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsSemiDiscretionary", "Is the position semi discretionary.", "", true);
                f.AddYesNo("Proposed", "Proposed Appointment", "", false);
            });
        }

        private void TypeDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddDecimal("AnnumAmount", "Per-annum Amount", true);
                f.AddPickList("RemunerationPeriod", "RemunerationPeriod", "Remuneration Period", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddPickList("AppointmentSource", "AppointmentSource", "Appointment Source", true);
                f.AddPickList("SelectionProcess", "SelectionProcess", "Selection Process", true);
            });

            field.AddFieldGroup(f =>
            {
                f.AddPickList("Judicial", "Judicial", "Judicial", false);
                f.AddNumeric("PrevTerms", "Previous Terms", false);
              
            });
            field.AddYesNo("ExclGenderReport", "Exclude Gender Report", "", false);
        }
        
    }
}