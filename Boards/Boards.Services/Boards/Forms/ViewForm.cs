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

            field.AddInput("Name", "Name", x =>
                {
                    x.AddRequired("Name is required");
                    x.AddRule(ValRule.Min(5, "Minimum 5 chars required"));
                    x.FieldType = FormFieldType.Text;
                });
            field.AddFieldGroup(f =>
            {

                f.AddInput("Acronym", "Acronym", false);
                f.AddLookup("AppTeam", "AppTeamLookup", Routes.Default, "Team/Division", true);
                f.AddLookup("Portfolio", "PortfolioLookup", Routes.Portfolios, "Portfolio", true);
                f.AddYesNo("ReportingApproved", "Approved for reporting", "", true);
            });
            field.AddInput("Description", "Description", x =>
            {
                x.AddRequired("Description is required");
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });

            field.AddInput("Summary", "Current summary", x =>
            {
                x.FieldType = FormFieldType.Note;
            });

            field.AddFieldGroup(f =>
            {
                f.AddPickList("OwnerDivision", "OwnerDivision", "Owner Division");
                f.AddInput("NominationCommittee", "Appointed By");
                f.AddInput("LegislationReference", "Legislation Reference");
                f.AddNumeric("MaximumTerms", "Total maximum years of service");

            });

            field.AddFieldGroup(f =>
            {
                f.AddPickList("EstablishedByUnder", "EstablishedByUnder", "Established by under");
                f.AddInput("QuorumRequiredText", "Quorum required text");
                f.AddNumeric("MinimumMembers", "Minimum members", true);
                f.AddNumeric("MaximumMembers", "Maximum members", true);

            });


            field.AddFieldGroup(f =>
            {
                f.AddLookup("ResponsibleUser", "ActiveUsers", Routes.Users, "Board Administrator");
                f.AddLookup("ApprovedUser", "ActiveUsers", Routes.Users, "Approved User");
                f.AddLookup("AsstSecretary", "SecretaryLookup", Routes.Secretary, "Asst. Secretary");
                f.AddInput("Constitution", "Website");
            });
            //--------------------

            // field.AddDivider(" ");
            //field.AddFieldGroup(f =>
            //{
            //    f.AddPickList("BoardStatus", "BoardStatus", "Board Status");
            //    f.AddPickList("OwnerPosition", "OwnerPosition", "Owner Position");
            //    f.AddInput("QuorumRequired", "Quorum required number");
            //    f.AddNumeric("OptimumMembers", "Optimum members");
            //});

            //field.AddInput("EstablishedByUnderText", "Established by under text", x =>
            //{
            //    x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
            //    x.FieldType = FormFieldType.Note;
            //});

            field.AddFieldGroup(f =>
            {

                // f.AddYesNo("ExcludeFromGenderBalance", "Exclude from gender balance", "", true);
            });


        }


    }
}