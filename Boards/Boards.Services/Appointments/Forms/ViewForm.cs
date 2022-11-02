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
                f.AddLookup("Appointee", "AppointeeLookup", Routes.Appointee, "Appointees", true);
                f.AddDate("StartDate", "Start Date", true);
                f.AddDate("EndDate", "End Date", false);
                f.AddPickList("Appointer", "Appointer", "Appointer/Approver", true);
            });

            field.AddFieldGroup(f =>
            {
                f.AddInput("BriefNumber", "Submission Number", true);
                f.AddPickList("AppointmentSource", "AppointmentSource", "Appointment Source", true);
                f.AddPickList("Judicial", "Judicial", "Judicial", false);
                f.AddYesNo("ActingInRole", "Acting", "", false);
            });

            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsExOfficio", "Ex Officio", "", true);
                f.AddYesNo("ExclGenderReport", "Exclude from gender balance", "", false);
                f.AddDate("AppointmentDate", "Appointment Approval Date", true);
                f.AddDate("InitialStartDate", "Initial Start Date", false);
            });

            field.AddFieldGroup(f =>
            {
                f.AddDecimal("AnnumAmount", "Remuneration", true);
                f.AddPickList("RemunerationPeriod", "RemunerationPeriod", "Remuneration Period", true);
                f.AddSelect<FullTimeEnum>("IsFullTime", "Is full time", false);
                f.AddYesNo("Proposed", "Proposed Appointment", "", false);
            });

            field.AddFieldGroup(f =>
            {
               
                f.AddYesNo("IsSemiDiscretionary", "Is the position semi discretionary.", "", false);
                f.AddNumeric("PrevTerms", "How many terms previously served?", false);
                f.AddFiller();
                f.AddFiller();
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