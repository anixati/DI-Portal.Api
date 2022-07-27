using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCustomMigrations.Db.Migrations
{
    public partial class Update_21072022_153953 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeadTimeToAppoint",
                schema: "Dbo",
                table: "BoardRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeadTimeToAppoint",
                schema: "Dbo",
                table: "BoardRoles");
        }
    }
}
