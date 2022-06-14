using Boards.Domain.Shared;
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
            fs.AddTab("Role Details", BoardDetails);
            fs.AddTab("Ministerial Details", MinisterialDetails);
            fs.AddTab("Other Details",OtherDetails);
            fs.AddSubgrid("Appointments", "RoleAppointmentsView", x =>
            {
                x.AddAction("create", Constants.Forms.BoardAppointment.Key, "New Appointment");
            });
        }

        private void OtherDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsFullTime", "Is full time", "", true, 35);
                f.AddYesNo("IsExecutive", "Is Executive", "", true, 35);
                f.AddYesNo("IsExOfficio", "Is Ex-Officio", "", false, 35);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsApsEmployee", "Is Aps Employee", "", true, 35);
                f.AddYesNo("IsExNominated", "Is Externally Nominated", "", true, 35);
                f.AddYesNo("IsSignAppointment", "Significant Appointment", "", true, 35);
            });
            
            field.AddFieldGroup(f =>
            {
               
                f.AddYesNo("ExcludeFromOrder15", "Exclude from order 15 report", "", true, 35);
                f.AddYesNo("ExcludeGenderReport", "Exclude from gender report", "", true,35);
            });
        }

        private void BoardDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddPickList("Position", "Position", "Position", true, 29);
                f.AddPickList("Appointer", "Appointer", "Appointer", true, 29);
                f.AddSelect<YesNoOptionEnum>("PositionRemunerated", "Position Remunerated", true,29);
                f.AddDecimal("PaAmount", "Per-annum Amount", true, 29);
            });
            field.AddFieldGroup(f =>
            {
                f.AddPickList("RemunerationMethod", "RemunerationMethod", "Remuneration Method", true, 29);
                f.AddInput("RemunerationTribunal", "Remuneration Tribunal", true, 29);
                f.AddDate("VacantFromDate", "Vacant from Date", false, 29);
                f.AddNumeric("Term", "Term",false,29);
            });
            field.AddInput("InstrumentLink", "Instrument Link", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
            field.AddInput("NextSteps", "Next Steps", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
         
        }

        private void MinisterialDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("PDMSNumber", "PDMS Number", true, 29);
                f.AddPickList("MinSubLocation", "MinSubLocation", "MinSub Location", true, 29);
                f.AddDate("MinisterOfficeDate", "Minister Office Date", false, 29);
                f.AddDate("MinisterActionDate", "Minister Action Date", false, 29);
            });

            field.AddFieldGroup(f =>
            {
                f.AddSelect<DateStateEnum>("LetterToPmDateType", "Letter to PM date type", false, 29);
                f.AddDate("LetterToPmDate", "Letter to PM date", false, 29);
                f.AddSelect<DateStateEnum>("ExCoDateType", "ExCo date type", false, 29);
                f.AddDate("ExCoDate", "ExCo Date", false, 29);
            });

            field.AddFieldGroup(f =>
            {
                f.AddSelect<DateStateEnum>("NotifyLetterDateType", "Notification letter date type", false, 29);
                f.AddDate("NotifyLetterDate", "Notification letter date", false, 29);
                f.AddSelect<DateStateEnum>("CabinetDateType", "Cabinet date type", false, 29);
                f.AddDate("CabinetDate", "Cabinet date", false, 29);
            });

            field.AddInput("InternalNotes", "Internal Notes", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });

        }
        


    }
}