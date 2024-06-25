using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace job_application_management_system_api.Models
{
    public class JobResponsibility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobResponsibilityID { get; set; }
        public int JobID { get; set; }
        public string? Responsibility { get; set; }
    }
}
