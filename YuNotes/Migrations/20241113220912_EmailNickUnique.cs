using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YuNotes.Migrations
{
    /// <inheritdoc />
    public partial class EmailNickUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("4daf417e-ebbb-477c-a914-796df78bc01a"), "Personal" },
                    { new Guid("7a8aca68-928b-45ac-9eef-e86432f0faa3"), "Life" },
                    { new Guid("a3150378-9ae2-4476-94fa-146f18fa8b93"), "Travel" },
                    { new Guid("ce5834ea-d559-450b-b02f-1223ad706528"), "Work" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Nickname",
                table: "Users",
                column: "Nickname",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Nickname",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("4daf417e-ebbb-477c-a914-796df78bc01a"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("7a8aca68-928b-45ac-9eef-e86432f0faa3"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("a3150378-9ae2-4476-94fa-146f18fa8b93"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("ce5834ea-d559-450b-b02f-1223ad706528"));

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
    }
}
