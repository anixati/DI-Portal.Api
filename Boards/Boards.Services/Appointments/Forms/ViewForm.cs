using AutoMapper.Internal;
using Boards.Domain.Shared;
using Boards.Services.Core;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Appointments.Forms
{
    public class ViewForm : BoardForms
    {
        public override string FormName => Constants.Forms.BoardAppointment.View;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Appointment", Appointment);
            fs.AddPage("Other Details", OtherDetails);
        }

        private void Appointment(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddLookup("Appointee", "AppointeeLookup",Routes.Appointee,"Appointees", true, 29);
                f.AddInput("BriefNumber", "Brief Number", true, 29);
                f.AddPickList("Judicial", "Judicial", "Judicial", false, 29);
                f.AddNumeric("PrevTerms", "Previous Terms", false, 29);
            });

            field.AddFieldGroup(f =>
            {
                f.AddDate("StartDate", "Start Date", true, 29);
                f.AddDate("EndDate", "End Date", false, 29);
                f.AddDate("AppointmentDate", "Appointment Date", true, 29);
                f.AddDate("InitialStartDate", "Initial Start Date", false, 29);
            });
            field.AddFieldGroup(f =>
            {
                f.AddDecimal("AnnumAmount", "Per-annum Amount", true, 29);
                f.AddPickList("RemunerationPeriod", "RemunerationPeriod", "Remuneration Period", true, 29);
                f.AddPickList("AppointmentSource", "AppointmentSource", "Appointment Source", true, 29);
                f.AddPickList("SelectionProcess", "SelectionProcess", "Selection Process", true, 29);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsExOfficio", "Is ExOfficio", "", true, 29);
                f.AddYesNo("IsFullTime", "Full Time", "", false, 29);
                f.AddYesNo("ActingInRole", "Acting", "", false, 29);
                f.AddYesNo("ExclGenderReport", "Exclude Gender Report", "", false, 29);
            });
           
            
        }

        private void OtherDetails(FormField field)
        {
            

        }


    }
}