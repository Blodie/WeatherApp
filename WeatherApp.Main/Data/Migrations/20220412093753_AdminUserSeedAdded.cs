using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Main.Data.Migrations
{
    public partial class AdminUserSeedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "357a6898-c94f-4ee7-b66d-d4689b82e88f", "486f338a-802e-48e7-8bfe-fe7c27864fe4", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "357a6898-c94f-4ee7-b66d-d4689b82e88f", 0, "b8e202cb-5e15-4709-a25e-7be2deabc4a8", "admin@admin.admin", true, false, null, "ADMIN@ADMIN.ADMIN", "admin", "AQAAAAEAACcQAAAAEPIhB2IvNvfFf0xLxPdnL4mAd0MBAhtDOocEm7JgW8rvAp2tL9b4airdPqW4wzmwfA==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "357a6898-c94f-4ee7-b66d-d4689b82e88f", "357a6898-c94f-4ee7-b66d-d4689b82e88f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "357a6898-c94f-4ee7-b66d-d4689b82e88f", "357a6898-c94f-4ee7-b66d-d4689b82e88f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "357a6898-c94f-4ee7-b66d-d4689b82e88f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "357a6898-c94f-4ee7-b66d-d4689b82e88f");
        }
    }
}
