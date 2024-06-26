using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobapplicationmanagementsystemapi.Migrations
{
    /// <inheritdoc />
    public partial class CreateJobApplicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobApplication",
                columns: table => new
                {
                    jobApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobID = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fathersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mothersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    permanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bscStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bscAdmissionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bscGraduationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mscStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mscAdmissionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mscGraduationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplication", x => x.jobApplicationID);
                });

            migrationBuilder.CreateTable(
                name: "UserSkill",
                columns: table => new
                {
                    userSkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobApplicationID = table.Column<int>(type: "int", nullable: false),
                    skill = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkill", x => x.userSkillID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplication");

            migrationBuilder.DropTable(
                name: "UserSkill");
        }
    }
}
