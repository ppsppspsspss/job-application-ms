namespace job_application_management_system_api.Models.DTOs
{
    public class UpdateOpeningDTO
    {
        public string? jobTitle { get; set; }
        public string? designation { get; set; }
        public string? jobType { get; set; }
        public string? workHourStart { get; set; }
        public string? workHourEnd { get; set; }
        public string? salary { get; set; }
        public string? negotiable { get; set; }
        public string? description { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? location { get; set; }
        public string? maxApplicants { get; set; }
        public string? deadline { get; set; }
        public string? status { get; set; }
        public List<string>? Requirements { get; set; }
        public List<string>? Responsibilities { get; set; }
    }
}
