using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassTracking.Server.Data.Migrations
{
    public partial class AddUserLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47e0f05c-104c-4a6e-9352-1205da7b622f", "2cc74a21-814d-4d57-b7d5-b6a474fcdaec", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1d3a6793-0b53-412c-9d8d-5ea6a3b2e6f3", 0, "6f049186-a020-4547-a912-bf4c47b50520", "admin@gmail.com", true, true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEL+DUqR3kfTbEmOrV9+A0hR2A9+iUbuBfRR/aaP/LKZkr/MpH//QrV3fMHH95kKyVw==", "01917698460", false, "R2T6KH3SFULI6OMKR7I6RFUTKNOX36EI", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "47e0f05c-104c-4a6e-9352-1205da7b622f", "1d3a6793-0b53-412c-9d8d-5ea6a3b2e6f3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "47e0f05c-104c-4a6e-9352-1205da7b622f", "1d3a6793-0b53-412c-9d8d-5ea6a3b2e6f3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47e0f05c-104c-4a6e-9352-1205da7b622f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d3a6793-0b53-412c-9d8d-5ea6a3b2e6f3");
        }
    }
}
