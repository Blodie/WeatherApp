using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Main.Data.Migrations
{
    public partial class FixAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedCities_Cities_CityId",
                table: "UserSelectedCities");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "UserSelectedCities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedCities_Cities_CityId",
                table: "UserSelectedCities",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedCities_Cities_CityId",
                table: "UserSelectedCities");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "UserSelectedCities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedCities_Cities_CityId",
                table: "UserSelectedCities",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
