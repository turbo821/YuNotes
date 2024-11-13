using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YuNotes.Migrations
{
    /// <inheritdoc />
    public partial class EmailIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2158b579-7fd7-41b3-98b7-3266283ce82e"), "Personal" },
                    { new Guid("4f42018a-b2b3-467b-8c26-50127d12a421"), "Travel" },
                    { new Guid("8b9b91d2-7458-4c47-8cf3-68e0d1fb7b1d"), "Life" },
                    { new Guid("fc0fb84f-fb3f-4ee1-aa2b-588a0f632973"), "Work" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("2158b579-7fd7-41b3-98b7-3266283ce82e"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("4f42018a-b2b3-467b-8c26-50127d12a421"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("8b9b91d2-7458-4c47-8cf3-68e0d1fb7b1d"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("fc0fb84f-fb3f-4ee1-aa2b-588a0f632973"));

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
        }
    }
}
