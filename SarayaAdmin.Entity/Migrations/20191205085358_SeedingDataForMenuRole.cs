using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SarayaAdmin.Entity.Migrations
{
    public partial class SeedingDataForMenuRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "credentials",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_on",
                value: new DateTime(2019, 12, 5, 15, 53, 57, 418, DateTimeKind.Local).AddTicks(4987));

            migrationBuilder.InsertData(
                table: "menu",
                columns: new[] { "id", "parent", "status", "title", "url" },
                values: new object[] { 1L, null, true, "Master Data", "#" });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name", "status" },
                values: new object[] { 1L, "System Admin", true });

            migrationBuilder.InsertData(
                table: "menu",
                columns: new[] { "id", "parent", "status", "title", "url" },
                values: new object[,]
                {
                    { 2L, 1L, true, "User", "/master/user" },
                    { 3L, 1L, true, "Menu", "/master/menu" },
                    { 4L, 1L, true, "Role", "/master/role" },
                    { 5L, 1L, true, "User Role Map", "/master/user-role-map" },
                    { 6L, 1L, true, "Menu Role Map", "/master/menu-role-map" }
                });

            migrationBuilder.InsertData(
                table: "menu_role_map",
                columns: new[] { "id", "menu_id", "role_id" },
                values: new object[] { 1L, 1L, 1L });

            migrationBuilder.InsertData(
                table: "role_map",
                columns: new[] { "id", "credential_id", "role_id" },
                values: new object[] { 1L, 1L, 1L });

            migrationBuilder.InsertData(
                table: "menu_role_map",
                columns: new[] { "id", "menu_id", "role_id" },
                values: new object[,]
                {
                    { 2L, 2L, 1L },
                    { 3L, 3L, 1L },
                    { 4L, 4L, 1L },
                    { 5L, 5L, 1L },
                    { 6L, 6L, 1L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "menu_role_map",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "menu_role_map",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "menu_role_map",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "menu_role_map",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "menu_role_map",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "menu_role_map",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "role_map",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "menu",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "menu",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "menu",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "menu",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "menu",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "menu",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "credentials",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_on",
                value: new DateTime(2019, 12, 4, 15, 51, 43, 426, DateTimeKind.Local).AddTicks(494));
        }
    }
}
