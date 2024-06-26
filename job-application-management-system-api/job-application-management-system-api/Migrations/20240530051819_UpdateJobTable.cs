using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobapplicationmanagementsystemapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "applicants",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "applicants",
                table: "Job");
        }
    }
}
