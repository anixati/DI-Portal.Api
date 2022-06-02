using Boards.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

// ReSharper disable once CheckNamespace
namespace EFCustomMigrations.Db.Migrations
{
    [DbContext(typeof(BoardsDbContext))]
    [Migration("CustomMigration_BoardDbViews")]
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
                    ap.[Disabled],
                    (SELECT os.Label FROM OptionSet os JOIN OptionKeys ok on os.OptionKeyId= ok.Id WHERE ok.Code='new_positiontype' AND os.Value= ap.CapabilitiesId) AS Capability,
                    (SELECT os.Label FROM OptionSet os JOIN OptionKeys ok on os.OptionKeyId= ok.Id WHERE ok.Code='new_positiontype' AND os.Value= ap.ExperienceId) AS Experience
                    FROM [dbo].[Appointee] ap
                    WHERE ap.Deleted=0
            ");
            
            migrationBuilder.Sql(@"
                CREATE VIEW MinistersView AS 
                    SELECT 
                    mns.Id,
                    (COALESCE(mns.Title+' ','')+mns.FirstName+' '+COALESCE(mns.MiddleName+' ','')+mns.LastName) As FullName,
                    (case mns.Gender when 1 then 'Male' when 2 then 'Female' else 'NA' end) As Gender,
                    mns.HomePhone AS Phone,
                    mns.MobilePhone AS Mobile,
                    mns.FaxNumber As Fax,
                    mns.Email1 as Email,
                    mns.StreetAddress_City AS City,
                    mns.StreetAddress_State AS State,
                    mns.[Disabled]
                    FROM [dbo].[Ministers] mns
                    WHERE mns.Deleted=0
            ");
            
            migrationBuilder.Sql(@"
                CREATE VIEW SecretariesView AS 
                    SELECT 
                    mns.Id,
                    (COALESCE(mns.Title+' ','')+mns.FirstName+' '+COALESCE(mns.MiddleName+' ','')+mns.LastName) As FullName,
                    (case mns.Gender when 1 then 'Male' when 2 then 'Female' else 'NA' end) As Gender,
                    mns.HomePhone AS Phone,
                    mns.MobilePhone AS Mobile,
                    mns.FaxNumber As Fax,
                    mns.Email1 as Email,
                    mns.StreetAddress_City AS City,
                    mns.StreetAddress_State AS State,
                    mns.[Disabled]
                    FROM [dbo].[Secretaries] mns
                    WHERE mns.Deleted=0
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW PortfoliosView AS 
                    SELECT
                    Pf.Id,
                    pf.[Name] AS PortfolioName,
                    pf.[Description] AS [Description],
                    pf.CreatedOn,
                    (COALESCE(mn.Title+' ','')+mn.FirstName+' '+COALESCE(mn.MiddleName+' ','')+mn.LastName) As Minister,
                    mt.StartDate,
                    mt.EndDate,
                    pf.[Disabled]
                    FROM [dbo].[Portfolios] pf
                    LEFT OUTER JOIN [dbo].[MinisterTerms] mt ON mt.PortfolioId = pf.Id
                    LEFT OUTER JOIN [dbo].[Ministers] mn ON mn.Id = mt.MinisterId
                    WHERE pf.Deleted =0
            ");

         

        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                        drop view AppointeesView;
                        drop view MinistersView;
                        drop view SecretariesView;
                        drop view PortfoliosView;
                        ");
        }
    }
}