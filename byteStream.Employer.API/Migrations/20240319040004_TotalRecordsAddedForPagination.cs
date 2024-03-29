using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace byteStream.Employer.API.Migrations
{
    /// <inheritdoc />
    public partial class TotalRecordsAddedForPagination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalRecords",
                table: "UserVacancyRequests",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalRecords",
                table: "UserVacancyRequests");
        }
    }
}
