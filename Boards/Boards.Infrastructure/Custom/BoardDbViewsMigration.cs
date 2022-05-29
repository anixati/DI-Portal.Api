using Boards.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

// ReSharper disable once CheckNamespace
namespace EFCustomMigrations.Db.Migrations
{
    [DbContext(typeof(BoardsDbContext))]
    [Migration("CustomMigration_ BoardDbViews")]
    public class BoardDbViewsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW  AppointeesView AS 
                    SELECT 
                    ap.Id,
                    (COALESCE(ap.Title+' ','')+ap.FirstName+' '+COALESCE(ap.MiddleName+' ','')+ap.LastName) As FullName,
                    (case ap.Gender when 1 then 'Male' when 2 then 'Female' else 'NA' end) As Gender,
                    ap.HomePhone AS Phone,
                    ap.MobilePhone AS Mobile,
                    ap.FaxNumber As Fax,
                    ap.Email1 as Email,
                    ap.StreetAddress_City AS City,
                    ap.StreetAddress_State AS State,
                    (case ap.IsAboriginal when 1 then 'Yes' when 2 then 'No' else '' end) AS Aboriginal,
                    (case ap.IsDisabled when 1 then 'Yes' when 2 then 'No' else '' end) AS Handicapped,
                    (case ap.IsRegional when 1 then 'Yes' when 2 then 'No' else '' end)   AS Regional,
                    (case ap.ExecutiveSearch when 1 then 'Yes' when 2 then 'No' else '' end)  AS Executive,
                    ap.Disabled,
                    (SELECT os.Label FROM OptionSet os JOIN OptionKeys ok on os.OptionKeyId= ok.Id WHERE ok.Code='new_positiontype' AND os.Value= ap.CapabilitiesId) AS Capability,
                    (SELECT os.Label FROM OptionSet os JOIN OptionKeys ok on os.OptionKeyId= ok.Id WHERE ok.Code='new_positiontype' AND os.Value= ap.ExperienceId) AS Experience
                    FROM [dbo].[Appointee] ap
                    WHERE ap.Deleted=0
            ");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                        drop view AppointeesView;
                        ");
        }
    }
}