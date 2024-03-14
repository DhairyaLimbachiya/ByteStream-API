using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace byteStream.Auth.Api.Migrations
{
    /// <inheritdoc />
    public partial class ExperienceFKError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09c340f3-9d22-4553-8f00-1c8f02e8d4db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4faeac64-f780-4ddf-b123-91a4c47b8dce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9810fea6-1335-4203-82e3-bda4e78dfbed");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "09c340f3-9d22-4553-8f00-1c8f02e8d4db", null, "Employer", "EMPLOYER" },
                    { "4faeac64-f780-4ddf-b123-91a4c47b8dce", null, "Admin", "ADMIN" },
                    { "9810fea6-1335-4203-82e3-bda4e78dfbed", null, "JobSeeker", "JOBSEEKER" }
                });
        }
    }
}
