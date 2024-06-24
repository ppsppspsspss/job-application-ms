using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace job_application_management_system_api.Models
{
    public class JobResponsibility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int jobResponsibilityID { get; set; }
        public int jobID { get; set; }
        public string? responsibility { get; set; }
    }
}
