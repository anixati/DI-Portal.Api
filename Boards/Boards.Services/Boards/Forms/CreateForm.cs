using Boards.Services.Client;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Boards.Forms
{
    public class CreateForm : FormBuilder
    {
        public override string FormName => Constants.Forms.Boards.Create;

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Board Details", AddDetails);
            fs.AddPage("Committee Details", ComitteeDetails);
            fs.AddPage("Legal Details", LegalDetails);
            fs.AddPage("Quorum Details", QuorumDetails);
            fs.AddPage("Administration", AdminDetails);
        }

        private void AddDetails(FormField field)
        {
            field.AddInput("Name", "Name", x =>
            {
                x.AddRequired("Name is required");
                x.AddRule(ValRule.Min(5, "Minimum 5 chars required"));
                x.FieldType = FormFieldType.Text;
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("Acronym", "Acronym");
                f.AddLookup("Portfolio", "PortfolioLookup", Routes.Portfolios, "Portfolio", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddLookup("AppTeam", "AppTeamLookup", Routes.Default, "Team/Division", true);
            });
            field.AddInput("Description", "Description", x =>
            {
                x.AddRequired("Description is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }

        private void ComitteeDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("NominationCommittee", "Nomination Committee");
                f.AddInput("LegislationReference", "Legislation Reference");
            });
            field.AddInput("Constitution", "Constitution");
            field.AddInput("Summary", "Summary", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }

        private void LegalDetails(FormField field)
        {
            field.AddInput("PendingAction", "Pending Action", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
            field.AddPickList("EstablishedByUnder", "EstablishedByUnder", "Established by under");
            field.AddInput("EstablishedByUnderText", "Established by under text", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }

        private void QuorumDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("QuorumRequired", "Quorum required number");
                f.AddInput("QuorumRequiredText", "Quorum required text");
            });
            field.AddFieldGroup(f =>
            {
                f.AddNumeric("OptimumMembers", "Optimum members");
                f.AddNumeric("MaximumTerms", "Total maximum years of service");
            });
            field.AddFieldGroup(f =>
            {
                f.AddNumeric("MaximumMembers", "Maximum members", true);
                f.AddNumeric("MinimumMembers", "Minimum members", true);
            });
            field.AddFieldGroup(f =>
            {
                f.AddYesNo("ReportingApproved", "Reporting approved", "Please specify", true);
                f.AddYesNo("ExcludeFromGenderBalance", "Exclude from gender balance", "Please specify", true);
            });


            field.AddFieldGroup(f =>
            {
                f.AddNumeric("MaxServicePeriod", "Maximum period of each term served by members");
            });
        }

        private void AdminDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddPickList("BoardStatus", "BoardStatus", "Board Status");
                f.AddPickList("OwnerDivision", "OwnerDivision", "Owner Division");
                f.AddPickList("OwnerPosition", "OwnerPosition", "Owner Position");
            });
            //field.AddFieldGroup(f =>
            //{
            //    f.AddPickList("Division", "Division", "Division");
            //});
            field.AddFieldGroup(f =>
            {
                f.AddLookup("ResponsibleUser", "ActiveUsers", Routes.Users, "Responsible User");
                f.AddLookup("ApprovedUser", "ActiveUsers", Routes.Users, "Approved User");
            });
            field.AddLookup("AsstSecretary", "SecretaryLookup", Routes.Secretary, "Asst. Secretary");
        }
    }
}