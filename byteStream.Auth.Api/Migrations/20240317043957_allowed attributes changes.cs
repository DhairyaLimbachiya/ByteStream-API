using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace byteStream.Auth.Api.Migrations
{
    /// <inheritdoc />
    public partial class allowedattributeschanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53e5174a-07ac-47d2-89c4-15a78424c14a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64363a37-fb2d-4ab1-a7fe-e02ee9d9cd3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee8f83f3-168b-46d8-9a0f-3e54ae403c93");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "63444c33-43fc-4a89-9d71-028ef656190e", null, "JobSeeker", "JOBSEEKER" },
                    { "cda46158-8928-414a-87c3-ed3aca147d81", null, "Employer", "EMPLOYER" },
                    { "dfba39d4-cfb7-4c3c-a3b9-612d42bba004", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63444c33-43fc-4a89-9d71-028ef656190e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cda46158-8928-414a-87c3-ed3aca147d81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfba39d4-cfb7-4c3c-a3b9-612d42bba004");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53e5174a-07ac-47d2-89c4-15a78424c14a", null, "Admin", "ADMIN" },
                    { "64363a37-fb2d-4ab1-a7fe-e02ee9d9cd3d", null, "Employer", "EMPLOYER" },
                    { "ee8f83f3-168b-46d8-9a0f-3e54ae403c93", null, "JobSeeker", "JOBSEEKER" }
                });
        }
    }
}
