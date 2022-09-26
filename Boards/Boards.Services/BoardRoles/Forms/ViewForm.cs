using Boards.Domain.Shared;
using Boards.Services.Client;
using Boards.Services.Core;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.BoardRoles.Forms
{
    public class ViewForm : BoardForms
    {
        public override string FormName => Constants.Forms.BoardRole.View;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddHeaders(f =>
            {
                f.AddLink("Board",  Routes.Boards, "Board");
            });

            fs.AddTab("Role Details", BoardDetails);
            fs.AddSubGrid("Appointments", "RoleAppointmentsView", x =>
            {
                x.AddAction("create", Constants.Forms.BoardAppointment.Key, "New Appointment");
            });
            fs.AddDocGrid(Constants.Entities.BoardRole);
        }
        private void BoardDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddDisabledInput("IncumbentName", "Current Appointee");
                f.AddPickList("Position", "Position", "Position", true);
                f.AddPickList("Appointer", "Appointer", "Appointer", true);
                f.AddNumeric("Term", "Term", false);
                

            });
            field.AddFieldGroup(f =>
            {
                f.AddDisabledInput("IncumbentStartDate", "Start Date");
                f.AddDisabledInput("IncumbentEndDate", "End Date");
                f.AddSelect<FullTimeEnum>("IsFullTime", "Is full time", true);
                f.AddYesNo("IsExOfficio", "Ex Officio", "", false);
            });

            field.AddFieldGroup(f =>
            {
                f.AddSelect<YesNoOptionEnum>("PositionRemunerated", "Position Remunerated", true);
                f.AddDecimal("PaAmount", "Remuneration", true);
                f.AddPickList("RemunerationMethod", "RemunerationMethod", "How is remuneration set", true);
                f.AddYesNo("IsApsEmployee", "APS Employee", "", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("ExcludeFromOrder15", "Exclude from senate order 15", "", true);
                f.AddYesNo("ExcludeGenderReport", "Exclude from GB", "", true);
                f.AddYesNo("IsSignAppointment", "Significant Appointment", "", true);
                f.AddYesNo("IsExNominated", "Is Externally Nominated", "", true);
            });
            field.AddDivider("Ministerial Details");

            field.AddFieldGroup(f =>
            {
                f.AddInput("PDMSNumber", "PDMS Number", true);
                f.AddPickList("MinSubLocation", "MinSubLocation", "MinSub Location", true);

                f.AddDate("MinisterOfficeDate", "Date Submission sent to MO", false);
                f.AddDate("MinisterActionDate", "Minister office due date", false);
            });

            field.AddInput("NextSteps", "Next Steps", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
            field.AddInput("ProcessStatus", "Process Status", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });

            field.AddFieldGroup(f =>
            {
                    f.AddSelect<DateStateEnum>("LetterToPmDateType", "Letter to PM date type", false);
                    f.AddSelect<DateStateEnum>("CabinetDateType", "Cabinet date type", false);
                    f.AddSelect<DateStateEnum>("ExCoDateType", "ExCo date type", false);
                    f.AddSelect<DateStateEnum>("NotifyLetterDateType", "Notification letter date type", false);
            });
            field.AddFieldGroup(f =>
            {
                f.AddDate("LetterToPmDate", "Date Letter sent to PM", false);
                f.AddDate("CabinetDate", "Date scheduled for Cabinet", false);
                f.AddDate("ExCoDate", "Date scheduled for Exco", false);
                f.AddDate("NotifyLetterDate", "Notification letter to appointee", false);
            });

            field.AddDivider("Sign off details");

            field.AddFieldGroup(f =>
            {
                f.AddDate("MinSubDate", "Date Minister signed instrument date");
                f.AddInput("InstrumentLink", "Instrument Link", true);
            });

            //------------------------------------------------------


            /*
            f.AddDate("VacantFromDate", "Vacant from Date", false);
            f.AddNumeric("Term", "Term", false);
            f.AddDate("VacantFromDate", "Vacant from Date", false);
          
            field.AddInput("InstrumentLink", "Instrument Link", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
            f.AddSelect<DateStateEnum>("MinSubDateType", "MinSub date type", true);
                f.AddYesNo("IsExecutive", "Is Executive", "", true, 35);
                f.AddYesNo("IsExNominated", "Is Externally Nominated", "", true, 35);
                f.AddNumeric("LeadTimeToAppoint", "Lead time to appointment");
            field.AddInput("InternalNotes", "Internal Notes", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
            */
        }



    }
}