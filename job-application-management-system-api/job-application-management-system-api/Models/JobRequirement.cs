using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace job_application_management_system_api.Models
{
    public class JobRequirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobRequirementID { get; set; }
        public int JobID { get; set; }
        public string? Requirement { get; set; }

    }
}
