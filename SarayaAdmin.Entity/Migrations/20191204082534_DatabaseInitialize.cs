using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SarayaAdmin.Entity.Migrations
{
    public partial class DatabaseInitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(maxLength: 100, nullable: false),
                    status = table.Column<bool>(nullable: false),
                    password = table.Column<string>(maxLength: 255, nullable: false),
                    username = table.Column<string>(maxLength: 50, nullable: false),
                    salt = table.Column<string>(maxLength: 255, nullable: true, defaultValueSql: "NULL::character varying"),
                    created_by = table.Column<string>(maxLength: 50, nullable: true),
                    created_on = table.Column<DateTime>(nullable: true),
                    created_host = table.Column<string>(maxLength: 20, nullable: true),
                    modified_by = table.Column<string>(maxLength: 50, nullable: true),
                    modified_on = table.Column<DateTime>(nullable: true),
                    modified_host = table.Column<string>(maxLength: 20, nullable: true),
                    verification_code = table.Column<string>(type: "character varying", nullable: true),
                    public_user = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credentials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(maxLength: 50, nullable: true),
                    url = table.Column<string>(maxLength: 100, nullable: true),
                    parent = table.Column<long>(nullable: true),
                    status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.id);
                    table.ForeignKey(
                        name: "fk_parent",
                        column: x => x.parent,
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "parameter",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    code = table.Column<string>(maxLength: 5, nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: false),
                    created_by = table.Column<string>(maxLength: 50, nullable: true),
                    created_on = table.Column<DateTime>(nullable: true),
                    created_host = table.Column<string>(maxLength: 20, nullable: true),
                    modified_by = table.Column<string>(maxLength: 50, nullable: true),
                    modified_on = table.Column<DateTime>(nullable: true),
                    modified_host = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parameter", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    credential_id = table.Column<long>(nullable: false),
                    first_name = table.Column<string>(maxLength: 50, nullable: true),
                    last_name = table.Column<string>(maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "character varying", nullable: true),
                    created_by = table.Column<string>(maxLength: 50, nullable: true),
                    created_on = table.Column<DateTime>(nullable: true),
                    created_host = table.Column<string>(maxLength: 20, nullable: true),
                    modified_by = table.Column<string>(maxLength: 50, nullable: true),
                    modified_on = table.Column<DateTime>(nullable: true),
                    modified_host = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "user_fk",
                        column: x => x.credential_id,
                        principalTable: "credentials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "menu_role_map",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    menu_id = table.Column<long>(nullable: false),
                    role_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_role_map", x => x.id);
                    table.ForeignKey(
                        name: "menu_role_map_fk",
                        column: x => x.menu_id,
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "menu_role_map_fk_1",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_map",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<long>(nullable: false),
                    credential_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_map", x => x.id);
                    table.ForeignKey(
                        name: "role_map_credentials_fk",
                        column: x => x.credential_id,
                        principalTable: "credentials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "role_map_role_fk",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_menu_parent",
                table: "menu",
                column: "parent");

            migrationBuilder.CreateIndex(
                name: "IX_menu_role_map_menu_id",
                table: "menu_role_map",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_menu_role_map_role_id",
                table: "menu_role_map",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_map_credential_id",
                table: "role_map",
                column: "credential_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_map_role_id",
                table: "role_map",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_credential_id",
                table: "user",
                column: "credential_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_role_map");

            migrationBuilder.DropTable(
                name: "parameter");

            migrationBuilder.DropTable(
                name: "role_map");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "menu");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "credentials");
        }
    }
}
