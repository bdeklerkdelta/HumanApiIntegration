using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthApp.Migrations
{
    public partial class Add_HumanId_To_UserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HumanId",
                table: "AbpUsers",
                type: "nvarchar(256)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HumanId",
                table: "AbpUsers");
        }
    }
}
