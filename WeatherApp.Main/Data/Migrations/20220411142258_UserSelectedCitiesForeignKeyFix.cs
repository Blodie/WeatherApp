using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Main.Data.Migrations
{
    public partial class UserSelectedCitiesForeignKeyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedCities_AspNetUsers_ApplicationUserId",
                table: "UserSelectedCities");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserSelectedCities",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedCities_AspNetUsers_ApplicationUserId",
                table: "UserSelectedCities",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSelectedCities_AspNetUsers_ApplicationUserId",
                table: "UserSelectedCities");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserSelectedCities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSelectedCities_AspNetUsers_ApplicationUserId",
                table: "UserSelectedCities",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
