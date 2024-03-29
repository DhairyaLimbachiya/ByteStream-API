using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace byteStream.Employer.API.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationStatus",
                table: "UserVacancyRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "UserVacancyRequests");
        }
    }
}
