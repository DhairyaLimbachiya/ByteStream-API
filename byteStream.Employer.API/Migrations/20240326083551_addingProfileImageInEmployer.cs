using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace byteStream.Employer.API.Migrations
{
    /// <inheritdoc />
    public partial class addingProfileImageInEmployer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Employers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Employers");
        }
    }
}
