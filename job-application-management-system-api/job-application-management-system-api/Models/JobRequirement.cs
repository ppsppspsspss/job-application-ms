using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace job_application_management_system_api.Models
{
    public class JobRequirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int jobRequirementID { get; set; }
        public int jobID { get; set; }
        public string? requirement { get; set; }

    }
}
