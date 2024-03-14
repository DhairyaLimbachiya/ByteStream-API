using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace byteStream.Auth.Api.Migrations
{
    /// <inheritdoc />
    public partial class datatypesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "839fb92c-e77c-43e6-8ee2-fc757a3dbbe5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b571369f-b4c7-4e65-8341-792c6b2051e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb353b73-75a2-4027-b258-005867653091");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "839fb92c-e77c-43e6-8ee2-fc757a3dbbe5", null, "Admin", "ADMIN" },
                    { "b571369f-b4c7-4e65-8341-792c6b2051e6", null, "JobSeeker", "JOBSEEKER" },
                    { "eb353b73-75a2-4027-b258-005867653091", null, "Employer", "EMPLOYER" }
                });
        }
    }
}
