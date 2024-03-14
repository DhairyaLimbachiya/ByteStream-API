using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace byteStream.JobSeeker.API.Migrations
{
    /// <inheritdoc />
    public partial class datatypesChanged2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "YearOfCompletion",
                table: "Qualifications",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YearOfCompletion",
                table: "Qualifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
