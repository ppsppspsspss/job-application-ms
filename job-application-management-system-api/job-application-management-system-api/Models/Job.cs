using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace job_application_management_system_api.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobID { get; set; }
        public string? JobTitle { get; set; }
        public string? Designation { get; set; }
        public string? JobType { get; set; }
        public string? WorkHourStart { get; set; }
        public string? WorkHourEnd { get; set; }
        public string? Salary { get; set; }
        public string? Negotiable { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public string? Applicants { get; set; }
        public string? MaxApplicants { get; set; }
        public string? PostedOn { get; set; }
        public string? Deadline { get; set; }
        public string? Status { get; set; }
    }
}
