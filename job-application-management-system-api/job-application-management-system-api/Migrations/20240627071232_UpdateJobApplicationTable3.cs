using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobapplicationmanagementsystemapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobApplicationTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "skill",
                table: "UserSkill",
                newName: "Skill");

            migrationBuilder.RenameColumn(
                name: "jobApplicationID",
                table: "UserSkill",
                newName: "JobApplicationID");

            migrationBuilder.RenameColumn(
                name: "userSkillID",
                table: "UserSkill",
                newName: "UserSkillID");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "User",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "User",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "fullname",
                table: "User",
                newName: "Fullname");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "User",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "responsibility",
                table: "JobResponsibility",
                newName: "Responsibility");

            migrationBuilder.RenameColumn(
                name: "jobID",
                table: "JobResponsibility",
                newName: "JobID");

            migrationBuilder.RenameColumn(
                name: "jobResponsibilityID",
                table: "JobResponsibility",
                newName: "JobResponsibilityID");

            migrationBuilder.RenameColumn(
                name: "requirement",
                table: "JobRequirement",
                newName: "Requirement");

            migrationBuilder.RenameColumn(
                name: "jobID",
                table: "JobRequirement",
                newName: "JobID");

            migrationBuilder.RenameColumn(
                name: "jobRequirementID",
                table: "JobRequirement",
                newName: "JobRequirementID");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "JobApplication",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "permanentAddress",
                table: "JobApplication",
                newName: "PermanentAddress");

            migrationBuilder.RenameColumn(
                name: "mscUniversity",
                table: "JobApplication",
                newName: "MscUniversity");

            migrationBuilder.RenameColumn(
                name: "mscStatus",
                table: "JobApplication",
                newName: "MscStatus");

            migrationBuilder.RenameColumn(
                name: "mscGraduationDate",
                table: "JobApplication",
                newName: "MscGraduationDate");

            migrationBuilder.RenameColumn(
                name: "mscGraduate",
                table: "JobApplication",
                newName: "MscGraduate");

            migrationBuilder.RenameColumn(
                name: "mscCGPA",
                table: "JobApplication",
                newName: "MscCGPA");

            migrationBuilder.RenameColumn(
                name: "mscAdmissionDate",
                table: "JobApplication",
                newName: "MscAdmissionDate");

            migrationBuilder.RenameColumn(
                name: "mscAIUBID",
                table: "JobApplication",
                newName: "MscAIUBID");

            migrationBuilder.RenameColumn(
                name: "mscAIUB",
                table: "JobApplication",
                newName: "MscAIUB");

            migrationBuilder.RenameColumn(
                name: "mothersName",
                table: "JobApplication",
                newName: "MothersName");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "JobApplication",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "jobID",
                table: "JobApplication",
                newName: "JobID");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "JobApplication",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "fathersName",
                table: "JobApplication",
                newName: "FathersName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "JobApplication",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "cv",
                table: "JobApplication",
                newName: "Cv");

            migrationBuilder.RenameColumn(
                name: "currentAddress",
                table: "JobApplication",
                newName: "CurrentAddress");

            migrationBuilder.RenameColumn(
                name: "bscUniversity",
                table: "JobApplication",
                newName: "BscUniversity");

            migrationBuilder.RenameColumn(
                name: "bscStatus",
                table: "JobApplication",
                newName: "BscStatus");

            migrationBuilder.RenameColumn(
                name: "bscGraduationDate",
                table: "JobApplication",
                newName: "BscGraduationDate");

            migrationBuilder.RenameColumn(
                name: "bscGraduate",
                table: "JobApplication",
                newName: "BscGraduate");

            migrationBuilder.RenameColumn(
                name: "bscCGPA",
                table: "JobApplication",
                newName: "BscCGPA");

            migrationBuilder.RenameColumn(
                name: "bscAdmissionDate",
                table: "JobApplication",
                newName: "BscAdmissionDate");

            migrationBuilder.RenameColumn(
                name: "bscAIUBID",
                table: "JobApplication",
                newName: "BscAIUBID");

            migrationBuilder.RenameColumn(
                name: "bscAIUB",
                table: "JobApplication",
                newName: "BscAIUB");

            migrationBuilder.RenameColumn(
                name: "jobApplicationID",
                table: "JobApplication",
                newName: "JobApplicationID");

            migrationBuilder.RenameColumn(
                name: "workHourStart",
                table: "Job",
                newName: "WorkHourStart");

            migrationBuilder.RenameColumn(
                name: "workHourEnd",
                table: "Job",
                newName: "WorkHourEnd");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Job",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "salary",
                table: "Job",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "postedOn",
                table: "Job",
                newName: "PostedOn");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Job",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "negotiable",
                table: "Job",
                newName: "Negotiable");

            migrationBuilder.RenameColumn(
                name: "maxApplicants",
                table: "Job",
                newName: "MaxApplicants");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "Job",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "jobType",
                table: "Job",
                newName: "JobType");

            migrationBuilder.RenameColumn(
                name: "jobTitle",
                table: "Job",
                newName: "JobTitle");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Job",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "designation",
                table: "Job",
                newName: "Designation");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Job",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "deadline",
                table: "Job",
                newName: "Deadline");

            migrationBuilder.RenameColumn(
                name: "applicants",
                table: "Job",
                newName: "Applicants");

            migrationBuilder.RenameColumn(
                name: "jobID",
                table: "Job",
                newName: "JobID");

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "JobApplication");

            migrationBuilder.RenameColumn(
                name: "Skill",
                table: "UserSkill",
                newName: "skill");

            migrationBuilder.RenameColumn(
                name: "JobApplicationID",
                table: "UserSkill",
                newName: "jobApplicationID");

            migrationBuilder.RenameColumn(
                name: "UserSkillID",
                table: "UserSkill",
                newName: "userSkillID");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "User",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "User",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "User",
                newName: "fullname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "User",
                newName: "userID");

            migrationBuilder.RenameColumn(
                name: "Responsibility",
                table: "JobResponsibility",
                newName: "responsibility");

            migrationBuilder.RenameColumn(
                name: "JobID",
                table: "JobResponsibility",
                newName: "jobID");

            migrationBuilder.RenameColumn(
                name: "JobResponsibilityID",
                table: "JobResponsibility",
                newName: "jobResponsibilityID");

            migrationBuilder.RenameColumn(
                name: "Requirement",
                table: "JobRequirement",
                newName: "requirement");

            migrationBuilder.RenameColumn(
                name: "JobID",
                table: "JobRequirement",
                newName: "jobID");

            migrationBuilder.RenameColumn(
                name: "JobRequirementID",
                table: "JobRequirement",
                newName: "jobRequirementID");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "JobApplication",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "PermanentAddress",
                table: "JobApplication",
                newName: "permanentAddress");

            migrationBuilder.RenameColumn(
                name: "MscUniversity",
                table: "JobApplication",
                newName: "mscUniversity");

            migrationBuilder.RenameColumn(
                name: "MscStatus",
                table: "JobApplication",
                newName: "mscStatus");

            migrationBuilder.RenameColumn(
                name: "MscGraduationDate",
                table: "JobApplication",
                newName: "mscGraduationDate");

            migrationBuilder.RenameColumn(
                name: "MscGraduate",
                table: "JobApplication",
                newName: "mscGraduate");

            migrationBuilder.RenameColumn(
                name: "MscCGPA",
                table: "JobApplication",
                newName: "mscCGPA");

            migrationBuilder.RenameColumn(
                name: "MscAdmissionDate",
                table: "JobApplication",
                newName: "mscAdmissionDate");

            migrationBuilder.RenameColumn(
                name: "MscAIUBID",
                table: "JobApplication",
                newName: "mscAIUBID");

            migrationBuilder.RenameColumn(
                name: "MscAIUB",
                table: "JobApplication",
                newName: "mscAIUB");

            migrationBuilder.RenameColumn(
                name: "MothersName",
                table: "JobApplication",
                newName: "mothersName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "JobApplication",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "JobID",
                table: "JobApplication",
                newName: "jobID");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "JobApplication",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "FathersName",
                table: "JobApplication",
                newName: "fathersName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "JobApplication",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Cv",
                table: "JobApplication",
                newName: "cv");

            migrationBuilder.RenameColumn(
                name: "CurrentAddress",
                table: "JobApplication",
                newName: "currentAddress");

            migrationBuilder.RenameColumn(
                name: "BscUniversity",
                table: "JobApplication",
                newName: "bscUniversity");

            migrationBuilder.RenameColumn(
                name: "BscStatus",
                table: "JobApplication",
                newName: "bscStatus");

            migrationBuilder.RenameColumn(
                name: "BscGraduationDate",
                table: "JobApplication",
                newName: "bscGraduationDate");

            migrationBuilder.RenameColumn(
                name: "BscGraduate",
                table: "JobApplication",
                newName: "bscGraduate");

            migrationBuilder.RenameColumn(
                name: "BscCGPA",
                table: "JobApplication",
                newName: "bscCGPA");

            migrationBuilder.RenameColumn(
                name: "BscAdmissionDate",
                table: "JobApplication",
                newName: "bscAdmissionDate");

            migrationBuilder.RenameColumn(
                name: "BscAIUBID",
                table: "JobApplication",
                newName: "bscAIUBID");

            migrationBuilder.RenameColumn(
                name: "BscAIUB",
                table: "JobApplication",
                newName: "bscAIUB");

            migrationBuilder.RenameColumn(
                name: "JobApplicationID",
                table: "JobApplication",
                newName: "jobApplicationID");

            migrationBuilder.RenameColumn(
                name: "WorkHourStart",
                table: "Job",
                newName: "workHourStart");

            migrationBuilder.RenameColumn(
                name: "WorkHourEnd",
                table: "Job",
                newName: "workHourEnd");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Job",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Job",
                newName: "salary");

            migrationBuilder.RenameColumn(
                name: "PostedOn",
                table: "Job",
                newName: "postedOn");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Job",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Negotiable",
                table: "Job",
                newName: "negotiable");

            migrationBuilder.RenameColumn(
                name: "MaxApplicants",
                table: "Job",
                newName: "maxApplicants");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Job",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "Job",
                newName: "jobType");

            migrationBuilder.RenameColumn(
                name: "JobTitle",
                table: "Job",
                newName: "jobTitle");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Job",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "Job",
                newName: "designation");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Job",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "Job",
                newName: "deadline");

            migrationBuilder.RenameColumn(
                name: "Applicants",
                table: "Job",
                newName: "applicants");

            migrationBuilder.RenameColumn(
                name: "JobID",
                table: "Job",
                newName: "jobID");
        }
    }
}
