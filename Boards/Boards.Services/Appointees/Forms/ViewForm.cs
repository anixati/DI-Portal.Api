using Boards.Services._Shared;
using Boards.Services.Core;
using DI.Domain.Enums;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Appointees.Forms
{
    public class ViewForm : BoardForms
    {
        public override string FormName => Constants.Forms.Appointee.View;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Personal Details", AddPersonDetails);
            fs.AddTab("Professional Details", AddProfessionalDetails);
            fs.AddTab("Other Details", OtherDetails);
            fs.AddSubGrid("Appointment's", "AppointeeAppointments", x =>
            {
            });
            fs.AddSubGrid("Skill's", "AppointeeSkills", x =>
            {
                x.AddAction("manage", "appointeeskill", "Manage Skill's");
            });
            fs.AddDocGrid(Constants.Entities.Appointee);
        }

        private void AddPersonDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("Title", "Title", false, 30);
                f.AddInput("FirstName", "First Name", true, 30);
                f.AddInput("LastName", "Last Name", true, 30);
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("MiddleName", "Middle Name", false, 30);
                f.AddSelect<GenderEnum>("Gender", "Gender", true, 30);

            });
            field.AddFieldGroup(f =>
            {
                f.AddPhone("Mobile", "Mobile Number", false, 30);
                f.AddPhone("HomePhone", "Phone Number", false, 30);
                f.AddPhone("FaxNumber", "Fax Number", false, 30);
            });
            field.AddFieldGroup(f =>
            {
                f.AddEmail("Email1", "Primary Email", true, 30);
                f.AddEmail("Email2", "Other Email", false, 30);
            });
            field.AddDivider("Street Address");
            field.AddAddress("StreetAddress");
            field.AddDivider("Postal Address");
            field.AddAddress("PostalAddress");

        }

        private void AddProfessionalDetails(FormField field)
        {
            field.AddInput("Biography", "Biography", x =>
            {
                x.AddRequired("Biography is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
            field.AddInput("PostNominals", "PostNominals");
            field.AddFieldGroup(f =>
            {
                f.AddInput("ResumeLink", "Resume Link");
                f.AddInput("LinkedInProfile", "LinkedIn Profile");
            });
        }

        private void OtherDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsRegional", "Regional", "Please specify", true);
                f.AddYesNo("IsAboriginal", "Aboriginal", "Please specify", true);
            });

            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsDisabled", "Disabled", "Please specify");
                f.AddYesNo("ExecutiveSearch", "Executive", "Please specify");
            });
        }
    }
}