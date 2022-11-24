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
            fs.AddSubGrid("Appointments", "AppointeeAppointments", x =>
            {
            });
            fs.AddSubGrid("Skills", "AppointeeSkills", x =>
            {
                x.AddAction("manage", "appointeeskill", "Manage Skills");
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
                f.AddInput("Title", "Title", false);
                f.AddInput("FirstName", "First Name", true);
                f.AddInput("MiddleName", "Middle Name", false);
                f.AddInput("LastName", "Last Name", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("PostNominals", "PostNominals");
                f.AddSelect<GenderEnum>("Gender", "Gender", true);
                f.AddPhone("HomePhone", "Phone", false);
                f.AddEmail("Email1", "Email", false);
            });
            field.AddFieldGroup(f =>
            {
                f.AddSelect<YesNoExEnum>("IsRegional", "Regional", true);
                f.AddSelect<YesNoExEnum>("IsAboriginal", "Aboriginal/Torres Strait Islander", true);
                f.AddSelect<YesNoExEnum>("IsDisabled", "Disabled", true);
                f.AddSelect<YesNoExEnum>("IsCAlDBackground", "CAlD Background", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddSelect<YesNoEnum>("ExecutiveSearch", "Executive Search", true);
                f.AddFiller();
                f.AddFiller();
                f.AddFiller();

            });
            //field.AddFieldGroup(f =>
            //{
            //    f.AddYesNo("IsRegional", "Regional", "", false);
            //    f.AddYesNo("IsAboriginal", "Aboriginal", "", false);
            //    f.AddYesNo("IsDisabled", "Disabled", "");
            //    f.AddYesNo("ExecutiveSearch", "Executive", "");
            //});

            field.AddDivider("Professional Details");
            field.AddInput("Biography", "Biography", x =>
            {
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