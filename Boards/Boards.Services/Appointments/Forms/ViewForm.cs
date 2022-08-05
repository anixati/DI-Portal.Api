using AutoMapper.Internal;
using Boards.Domain.Shared;
using Boards.Services.Client;
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
            fs.AddHeaders(f =>
            {
                f.AddLink("Board", Routes.Boards, "Board");
                f.AddLink("BoardRole", Routes.Roles, "Role");
            });

            fs.AddTab("Appointment", Appointment);
          //  fs.AddTab("Other Details", OtherDetails);
            fs.AddDocGrid(Constants.Entities.BoardAppointment);
        }

        private void Appointment(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddLookup("Appointee", "AppointeeLookup",Routes.Appointee,"Appointees", true);
                f.AddInput("BriefNumber", "Brief Number", true);
                f.AddPickList("Appointer", "Appointer", "Appointer/Approver", true, 50);
            });

            field.AddFieldGroup(f =>
            {
                f.AddPickList("Judicial", "Judicial", "Judicial", false);
                f.AddPickList("AppointmentSource", "AppointmentSource", "Appointment Source", true);
                f.AddPickList("SelectionProcess", "SelectionProcess", "Selection Process", true);
            });
            
            field.AddDivider("Appointment Dates");
            field.AddFieldGroup(f =>
            {
                f.AddDate("StartDate", "Start Date", true);
                f.AddDate("EndDate", "End Date", false);
                f.AddDate("AppointmentDate", "Appointment Date", true);
                f.AddDate("InitialStartDate", "Initial Start Date", false);
            });

            field.AddDivider("Remuneration Details");
            field.AddFieldGroup(f =>
            {
                f.AddDecimal("AnnumAmount", "Remuneration", true);
                f.AddPickList("RemunerationPeriod", "RemunerationPeriod", "Remuneration Period", true);
            });

            field.AddFieldGroup(f =>
            {
               
                f.AddNumeric("PrevTerms", "How many terms the member has previously served? (Not including this appointment)", false);
            });
            field.AddDivider("Reporting Details");
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsExOfficio", "Is ExOfficio", "", true);
                f.AddYesNo("IsFullTime", "Full Time", "", false);
                f.AddYesNo("ActingInRole", "Acting", "", false);
                f.AddYesNo("IsSemiDiscretionary", "Is the position semi discretionary.", "", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("ExclGenderReport", "Exclude from gender balance report", "", false);
                f.AddYesNo("Proposed", "Proposed Appointment", "", false);
            });

        }

        //private void OtherDetails(FormField field)
        //{
        //    field.AddYesNo("IsExOfficio", "Is ExOfficio", "", true);
        //    field.AddYesNo("IsFullTime", "Full Time", "", false);
        //    field.AddYesNo("ActingInRole", "Acting", "", false);
        //    field.AddYesNo("ExclGenderReport", "Exclude Gender Report", "", false);
        //}


    }
}