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
            fs.AddTab("Addresses", AddAddressDetails);
            fs.AddSubGrid("Appointment's", "AppointeeAppointments", x =>
            {
            });
            fs.AddSubGrid("Skill's", "AppointeeSkills", x =>
            {
                x.AddAction("manage", "appointeeskill", "Manage Skill's");
            });
            fs.AddDocGrid(Constants.Entities.Appointee);
        }
        private void AddAddressDetails(FormField field)
        {
            field.AddDivider("Street Address");
            field.AddAddress("StreetAddress", true);
            field.AddDivider("Postal Address");
            field.AddAddress("PostalAddress", false);
        }
        private void AddPersonDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("Title", "Title", false, 30);
                f.AddInput("FirstName", "First Name", true, 30);
                f.AddInput("MiddleName", "Middle Name", false, 30);
                f.AddInput("LastName", "Last Name", true, 30);
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("PostNominals", "PostNominals");
                f.AddSelect<GenderEnum>("Gender", "Gender", true, 30);
                f.AddPhone("HomePhone", "Phone", false, 30);
                f.AddEmail("Email1", "Email", true, 30);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("IsRegional", "Regional", "", true);
                f.AddYesNo("IsAboriginal", "Aboriginal", "", true);
                f.AddYesNo("IsDisabled", "Disabled", "");
                f.AddYesNo("ExecutiveSearch", "Executive", "");
            });

            field.AddDivider("Professional Details");
            field.AddInput("Biography", "Biography", x =>
            {
                x.AddRequired("Biography is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });

            field.AddFieldGroup(f =>
            {
                f.AddInput("ResumeLink", "Resume Link");
                f.AddInput("LinkedInProfile", "LinkedIn Profile");
            });


        }
    }
}