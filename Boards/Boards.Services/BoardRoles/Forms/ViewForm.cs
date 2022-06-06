using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.BoardRoles.Forms
{
    public class ViewForm : FormBuilder
    {
        public override string FormName => Constants.Forms.BoardRole.View;
        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddTab("Board Details", BoardDetails);
            fs.AddTab("Summary Details", SummaryDetails);
            fs.AddTab("Other Details", OtherDetails);
         
        }

        private void BoardDetails(FormField field)
        {
            field.AddInput("Name", "Name", x =>
            {
                x.AddRequired("Name is required");
                x.AddRule(ValRule.Min(5, "Minimum 5 chars required"));
                x.FieldType = FormFieldType.Text;
                x.Width = 98;
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("Acronym", "Acronym", true, 33);
                f.AddPickList("BoardStatus", "BoardStatus", "Board Status", false, 33);
                f.AddPickList("Division", "Division", "Division", false, 33);
            });
            field.AddFieldGroup(f =>
            {
                f.AddLookup("Portfolio", "PortfolioLookup", "Portfolio", true,33);
                f.AddPickList("OwnerDivision", "OwnerDivision", "Owner Division",false,33);
                f.AddPickList("OwnerPosition", "OwnerPosition", "Owner Position",false,33);
            });
            field.AddFieldGroup(f =>
            {
                f.AddInput("NominationCommittee", "Nomination Committee", false, 33);
                f.AddInput("LegislationReference", "Legislation Reference", false, 33);
                f.AddInput("Constitution", "Constitution", false, 33);
            });
            field.AddPickList("EstablishedByUnder", "EstablishedByUnder", "Established by under");
            field.AddInput("EstablishedByUnderText", "Established by under text", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
        }

        private void SummaryDetails(FormField field)
        {
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
            field.AddInput("PendingAction", "Pending Action", x =>
            {
                x.AddRule(ValRule.Min(10, "Minimum 10 chars required"));
                x.FieldType = FormFieldType.Note;
            });
          

        }


        private void OtherDetails(FormField field)
        {
            field.AddFieldGroup(f =>
            {
                f.AddInput("QuorumRequired", "Quorum required number",false,24);
                f.AddInput("QuorumRequiredText", "Quorum required text", false, 24);
                f.AddYesNo("ReportingApproved", "Reporting approved", "", true,24);
                f.AddYesNo("ExcludeFromGenderBalance", "Exclude from gender balance", "", true,24);

            });
            field.AddFieldGroup(f =>
            {
                f.AddNumeric("OptimumMembers", "Optimum members", false, 24);
                f.AddNumeric("MaximumTerms", "Maximum terms", false, 24);
                f.AddNumeric("MaximumMembers", "Maximum members", true,24);
                f.AddNumeric("MinimumMembers", "Minimum members", true,24);

            });
          
           
            field.AddFieldGroup(f =>
            {
                f.AddLookup("ResponsibleUser", "UsersLookup", "Responsible User",false,24);
                f.AddLookup("ApprovedUser", "UsersLookup", "Approved User", false, 24);
                f.AddLookup("AsstSecretary", "SecretaryLookup", "Asst. Secretary", false, 24);
            });
        }


    }
}