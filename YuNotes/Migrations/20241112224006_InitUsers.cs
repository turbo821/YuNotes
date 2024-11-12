using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YuNotes.Migrations
{
    /// <inheritdoc />
    public partial class InitUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("0ecb5278-cf64-446e-9908-56b92e4d7286"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("c6b2faf3-18e2-4793-83c7-918d1e92f826"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("c71d33fb-4879-4e66-b5f3-2e642ad8c092"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("dce9243e-ff2a-4cf6-8ed0-6500bb59667d"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Notes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nickname = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7add0b45-271b-447a-8cf1-d7f240b13064"), "Work" },
                    { new Guid("8038a2f9-580a-47cc-a380-408fd0aee079"), "Life" },
                    { new Guid("8ffbc4ed-4264-4110-b72f-528492020180"), "Travel" },
                    { new Guid("eae4954a-8ec7-4bf9-975e-b96a36fbf718"), "Personal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Notes_UserId",
                table: "Notes");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("7add0b45-271b-447a-8cf1-d7f240b13064"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("8038a2f9-580a-47cc-a380-408fd0aee079"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("8ffbc4ed-4264-4110-b72f-528492020180"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("eae4954a-8ec7-4bf9-975e-b96a36fbf718"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notes");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0ecb5278-cf64-446e-9908-56b92e4d7286"), "Personal" },
                    { new Guid("c6b2faf3-18e2-4793-83c7-918d1e92f826"), "Travel" },
                    { new Guid("c71d33fb-4879-4e66-b5f3-2e642ad8c092"), "Life" },
                    { new Guid("dce9243e-ff2a-4cf6-8ed0-6500bb59667d"), "Work" }
                });
        }
    }
}
