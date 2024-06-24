using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobapplicationmanagementsystemapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Job");
        }
    }
}
