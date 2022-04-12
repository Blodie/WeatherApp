using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Main.Data.Migrations
{
    public partial class AdminUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c9667285-bdf7-471c-9dcc-5d916a957596", "7d242e6d-fa56-4987-9f9b-26569d6b3105", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c9667285-bdf7-471c-9dcc-5d916a957596", 0, "8ec72147-6074-4fb6-b6db-23cfa013959a", "admin@admin.admin", true, false, null, "ADMIN@ADMIN.ADMIN", "ADMIN@ADMIN.ADMIN", "AQAAAAEAACcQAAAAENykC2EbAxAtUMVu9u7U1ykA+wUqHz46ZW7qAOnYb8rd+GDu5eB05gfuEz9DPI/wsw==", null, false, "", false, "admin@admin.admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c9667285-bdf7-471c-9dcc-5d916a957596", "c9667285-bdf7-471c-9dcc-5d916a957596" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c9667285-bdf7-471c-9dcc-5d916a957596", "c9667285-bdf7-471c-9dcc-5d916a957596" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9667285-bdf7-471c-9dcc-5d916a957596");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9667285-bdf7-471c-9dcc-5d916a957596");

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
    }
}
