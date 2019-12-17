using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SarayaAdmin.Entity.Migrations
{
    public partial class CredentialSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "credentials",
                columns: new[] { "id", "created_by", "created_host", "created_on", "email", "modified_by", "modified_host", "modified_on", "password", "public_user", "salt", "status", "username", "verification_code" },
                values: new object[] { 1L, "System", null, new DateTime(2019, 12, 4, 15, 51, 43, 426, DateTimeKind.Local).AddTicks(494), "admin@admin.com", null, null, null, "CiIzQaA1vmGP2Pkrcp2CDVelxx2w4Nw3qgvkgwcrf1c=", null, "Ef+5WLoTHpQ3nGH+uDko+w==", true, "administrator", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "credentials",
                keyColumn: "id",
                keyValue: 1L);
        }
    }
}
