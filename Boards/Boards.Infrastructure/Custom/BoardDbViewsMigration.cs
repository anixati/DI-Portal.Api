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
        

        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}