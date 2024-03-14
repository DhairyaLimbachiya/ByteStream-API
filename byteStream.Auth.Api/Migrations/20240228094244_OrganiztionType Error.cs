using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace byteStream.Auth.Api.Migrations
{
    /// <inheritdoc />
    public partial class OrganiztionTypeError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43dd7337-d954-4157-8a9c-a718fb557028");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "515d135c-2779-44ba-a00c-90351d3e4cf5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5f517ce-4036-43b7-a0e7-8c21a86cae7f");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "43dd7337-d954-4157-8a9c-a718fb557028", null, "Admin", "ADMIN" },
                    { "515d135c-2779-44ba-a00c-90351d3e4cf5", null, "JobSeeker", "JOBSEEKER" },
                    { "c5f517ce-4036-43b7-a0e7-8c21a86cae7f", null, "Employer", "EMPLOYER" }
                });
        }
    }
}
