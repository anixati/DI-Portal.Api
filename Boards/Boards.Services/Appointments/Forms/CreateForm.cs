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
                f.AddLookup("Appointee", "AppointeeLookup", Routes.Appointee, "Appointees", true,50);
                f.AddInput("BriefNumber", "Brief Number", true, 50);
            });
            
            field.AddFieldGroup(f =>
            {
                f.AddDate("StartDate", "Start Date", true, 50);
                f.AddDate("EndDate", "End Date", false, 50);
            });
            field.AddFieldGroup(f =>
            {
                f.AddDate("AppointmentDate", "Appointment Date", true, 50);
                f.AddDate("InitialStartDate", "Initial Start Date", false, 50);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsExOfficio", "Is ExOfficio", "", true, 30);
                f.AddYesNo("IsFullTime", "Full Time", "", false, 30);
                f.AddYesNo("ActingInRole", "Acting", "", false, 30);
            });
        }

        private void TypeDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddDecimal("AnnumAmount", "Per-annum Amount", true, 50);
                f.AddPickList("RemunerationPeriod", "RemunerationPeriod", "Remuneration Period", true, 50);
            });
            field.AddFieldGroup(f =>
            {
                f.AddPickList("AppointmentSource", "AppointmentSource", "Appointment Source", true, 50);
                f.AddPickList("SelectionProcess", "SelectionProcess", "Selection Process", true, 50);
            });

            field.AddFieldGroup(f =>
            {
                f.AddPickList("Judicial", "Judicial", "Judicial", false, 50);
                f.AddNumeric("PrevTerms", "Previous Terms", false, 50);
              
            });
            field.AddYesNo("ExclGenderReport", "Exclude Gender Report", "", false, 50);
        }
        
    }
}