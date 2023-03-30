using Boards.Domain.Shared;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.BoardRoles.Forms
{
    public class CreateForm : FormBuilder
    {
        public override string FormName => Constants.Forms.BoardRole.Create;

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Role Details", AddDetails);
            fs.AddPage("Role Type", TypeDetails);
            fs.AddPage("Ministerial Details", MinisterialDetails);
            fs.AddPage("Ministerial Dates", MinisterialDates);
            fs.AddPage("Notes", Notes);
        }

        private void AddDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddPickList("Position", "Position", "Position", true);
                f.AddPickList("RoleAppointer", "RoleAppointer", "Appointer", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddSelect<YesNoOptionEnum>("PositionRemunerated", "Position Remunerated", true);
                f.AddDecimal("PaAmount", "Per-annum Amount", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddPickList("RemunerationMethod", "RemunerationMethod", "Remuneration Method", true);
                f.AddInput("RemunerationTribunal", "Remuneration Tribunal", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddDate("VacantFromDate", "Vacant from Date", false);
                f.AddNumeric("Term", "Term");
            });
        }

        private void TypeDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddSelect<FullTimeEnum>("IsFullTime", "Is full time", false);
                f.AddYesNo("IsExecutive", "Is Executive", "", true);
                f.AddYesNo("IsExOfficio", "Is Ex-Officio", "", false);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsApsEmployee", "Is Aps Employee", "", true);
                f.AddYesNo("IsExNominated", "Is Externally Nominated", "", true);
                f.AddYesNo("IsSignAppointment", "Significant Appointment", "", true);
            });

            field.AddFieldGroup(f =>
            {
                f.AddYesNo("ExcludeFromOrder15", "Exclude from order 15 report", "", true);
                f.AddYesNo("ExcludeGenderReport", "Exclude from gender report", "", true);
            });
        }

        private void MinisterialDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("PDMSNumber", "PDMS Number", true);
                f.AddPickList("MinSubLocation", "MinSubLocation", "MinSub Location", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddDate("MinisterOfficeDate", "Minister Office Date", false);
                f.AddDate("MinisterActionDate", "Minister Action Date", false);
            });
            field.AddInput("InstrumentLink", "Instrument Link", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }

        private void MinisterialDates(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddSelect<DateStateEnum>("MinSubDateType", "MinSub date type", true);
                f.AddDate("MinSubDate", "MinSub date");
            });


            field.AddFieldGroup(f =>
            {
                f.AddSelect<DateStateEnum>("LetterToPmDateType", "Letter to PM date type", true);
                f.AddDate("LetterToPmDate", "Letter to PM date");
            });

            field.AddFieldGroup(f =>
            {
                f.AddSelect<DateStateEnum>("ExCoDateType", "ExCo date type", true);
                f.AddDate("ExCoDate", "ExCo Date");
            });

            field.AddFieldGroup(f =>
            {
                f.AddSelect<DateStateEnum>("NotifyLetterDateType", "Notification letter date type", true);
                f.AddDate("NotifyLetterDate", "Notification letter date");
            });
        }

        private void Notes(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddSelect<DateStateEnum>("CabinetDateType", "Cabinet date type", true);
                f.AddDate("CabinetDate", "Cabinet date");
            });
            //field.AddInput("ProcessStatus", "Process Status", x =>
            //{
            //    x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
            //    x.FieldType = FormFieldType.Note;
            //});
            field.AddInput("NextSteps", "Next Steps", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
            field.AddInput("InternalNotes", "Internal Notes", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }
    }
}