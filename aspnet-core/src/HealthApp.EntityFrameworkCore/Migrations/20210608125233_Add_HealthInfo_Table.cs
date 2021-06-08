using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthApp.Migrations
{
    public partial class Add_HealthInfo_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "bigint", nullable: false),
                    HumanId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Steps = table.Column<int>(type: "int", nullable: false),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vigorous = table.Column<double>(type: "float", nullable: false),
                    Moderate = table.Column<double>(type: "float", nullable: false),
                    Light = table.Column<double>(type: "float", nullable: false),
                    Sedentary = table.Column<double>(type: "float", nullable: false),
                    TrackerCalories = table.Column<double>(type: "float", nullable: false),
                    TrackerSteps = table.Column<int>(type: "int", nullable: false),
                    TrackerDistance = table.Column<double>(type: "float", nullable: false),
                    TrackerFloors = table.Column<double>(type: "float", nullable: false),
                    TrackerElevation = table.Column<double>(type: "float", nullable: false),
                    CaloriesBMR = table.Column<double>(type: "float", nullable: false),
                    ActivityCalories = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthInfos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
               name: "FK_HealthInfos_UserId",
               table: "HealthInfos",
               column: "UserId",
               principalTable: "AbpUsers",
               principalColumn: "Id",
               onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthInfos");
        }
    }
}
