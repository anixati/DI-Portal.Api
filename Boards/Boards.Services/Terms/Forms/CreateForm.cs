using Boards.Services.Client;
using DI.Forms.Handlers;
using DI.Forms.Types;

namespace Boards.Services.Terms.Forms
{
    public class CreateForm : FormBuilder
    {
        public override string FormName => Constants.Forms.MinisterTerm.Create;

        protected override void CreateSchema(FormSchema fs)
        {
            fs.AddPage("Term Details", TermDetails);
        }

        private void TermDetails(FormField field)
        {
            field.AddLookup("Portfolio", "PortfolioLookup", Routes.Default, "Portfolio", true, true,
                x => { x.Readonly = true; });
            field.AddLookup("Minister", "MinisterLookup", Routes.Default, "Minister", true);
            field.AddDate("StartDate", "Start Date", true);
            field.AddDate("EndDate", "End Date", false);
        }
    }
}