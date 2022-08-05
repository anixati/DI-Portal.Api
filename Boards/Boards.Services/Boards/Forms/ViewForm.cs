using Boards.Services.Client;
using Boards.Services.Core;
using DI.Domain.Enums;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Boards.Forms
{
    public class ViewForm : BoardForms
    {
        public override string FormName => Constants.Forms.Boards.View;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Board Details", BoardDetails);
            fs.AddSubGrid("Roles", "BoardRolesView", x =>
            {
                x.AddAction("create", Constants.Forms.BoardRole.Key, "New Role");
            });
            fs.AddSubGrid("Appointments", "BoardAppointmentsView", x =>
            {

            });
            fs.AddDocGrid(Constants.Entities.Board);
        }

        private void BoardDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("Name", "Name", x =>
                {
                    x.AddRequired("Name is required");
                    x.AddRule(ValRule.Min(5, "Minimum 5 chars required"));
                    x.FieldType = FormFieldType.Text;
                });
                f.AddInput("Acronym", "Acronym", true);
                f.AddInput("Constitution", "Website");
            });
            field.AddFieldGroup(f =>
            {
                f.AddLookup("Portfolio", "PortfolioLookup", Routes.Portfolios, "Portfolio", true);
                f.AddPickList("Division", "Division", "Division");
                f.AddLookup("AppTeam", "AppTeamLookup", Routes.Default, "Team/Division", true);
            });
            field.AddInput("Description", "Description", x =>
            {
                x.AddRequired("Description is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });

            field.AddInput("Summary", "Summary", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
            field.AddFieldGroup(f =>
            {
                f.AddPickList("BoardStatus", "BoardStatus", "Board Status");
                f.AddPickList("OwnerDivision", "OwnerDivision", "Owner Division");
                f.AddPickList("OwnerPosition", "OwnerPosition", "Owner Position");
            });

            field.AddFieldGroup(f =>
            {
                f.AddInput("NominationCommittee", "Nomination Committee");
                f.AddInput("LegislationReference", "Legislation Reference");
                f.AddPickList("EstablishedByUnder", "EstablishedByUnder", "Established by under");
            });
            field.AddInput("EstablishedByUnderText", "Established by under text", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });



            field.AddDivider("Quorum");
            field.AddFieldGroup(f =>
            {
                f.AddInput("QuorumRequired", "Quorum required number");
                f.AddInput("QuorumRequiredText", "Quorum required text");

            });

            field.AddDivider("Members");

            field.AddFieldGroup(f =>
            {
                f.AddNumeric("OptimumMembers", "Optimum members");
                f.AddNumeric("MaximumTerms", "Maximum terms");
                f.AddNumeric("MaximumMembers", "Maximum members", true);
                f.AddNumeric("MinimumMembers", "Minimum members", true);

            });
            field.AddFieldGroup(f =>
            {
                f.AddNumeric("MaxServicePeriod", "Maximum period of each term served by members");
                f.AddYesNo("ReportingApproved", "Reporting approved", "", true);
                f.AddYesNo("ExcludeFromGenderBalance", "Exclude from gender balance", "", true);
            });

            field.AddDivider("Office Details");

            field.AddFieldGroup(f =>
            {
                f.AddLookup("ResponsibleUser", "ActiveUsers", Routes.Users, "Responsible User");
                f.AddLookup("ApprovedUser", "ActiveUsers", Routes.Users, "Approved User");
                f.AddLookup("AsstSecretary", "SecretaryLookup", Routes.Secretary, "Asst. Secretary");
            });


        }


    }
}