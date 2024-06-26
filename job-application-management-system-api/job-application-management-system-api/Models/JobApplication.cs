using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace job_application_management_system_api.Models
{
    public class JobApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobApplicationID { get; set; }
        public int JobID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FathersName { get; set; }
        public string? MothersName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? CurrentAddress { get; set; }
        public string? PermanentAddress { get; set; }
        public string? BscStatus { get; set; }
        public string? BscAdmissionDate { get; set; }
        public string? BscAIUB { get; set; }
        public string? BscAIUBID { get; set; }
        public string? BscUniversity { get; set; }
        public string? BscCGPA { get; set; }
        public string? BscGraduate { get; set; }
        public string? BscGraduationDate { get; set; }
        public string? MscStatus { get; set; }
        public string? MscAdmissionDate { get; set; }
        public string? MscAIUB { get; set; }
        public string? MscAIUBID { get; set; }
        public string? MscUniversity { get; set; }
        public string? MscCGPA { get; set; }
        public string? MscGraduate { get; set; }
        public string? MscGraduationDate { get; set; }
        public string? Cv { get; set; }

    }
}