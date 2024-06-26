using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobapplicationmanagementsystemapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobApplicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bscAIUB",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bscAIUBID",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bscCGPA",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bscGraduate",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bscUniversity",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mscAIUB",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mscAIUBID",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mscCGPA",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mscGraduate",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mscUniversity",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bscAIUB",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "bscAIUBID",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "bscCGPA",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "bscGraduate",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "bscUniversity",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "mscAIUB",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "mscAIUBID",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "mscCGPA",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "mscGraduate",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "mscUniversity",
                table: "JobApplication");
        }
    }
}
