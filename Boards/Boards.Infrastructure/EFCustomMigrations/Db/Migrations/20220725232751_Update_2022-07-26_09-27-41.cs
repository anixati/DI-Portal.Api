using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCustomMigrations.Db.Migrations
{
    public partial class Update_20220726_092741 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                schema: "Dbo",
                table: "BoardAppointments",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                schema: "Dbo",
                table: "BoardAppointments");
        }
    }
}
