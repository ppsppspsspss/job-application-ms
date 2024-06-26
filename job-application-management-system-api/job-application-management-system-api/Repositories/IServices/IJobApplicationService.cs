using job_application_management_system_api.Models;
using job_application_management_system_api.Models.DTOs;
using job_application_management_system_api.Utils;
using SocialMedia.API.Models.DTOs;

namespace job_application_management_system_api.Repositories.IServices
{
    public interface IJobApplicationService
    {
        Result<string> Application(JobApplicationDTO jobApplicationDTO);
        Result<List<GetJobApplicationDTO>> GetAllJobApplications(int jobID);
        Result<GetJobApplicationDTO> GetJobApplication(int jobApplicationID);
    }
}
