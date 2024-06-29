using job_application_management_system_api.Models.DTOs;
using job_application_management_system_api.Repositories.IServices;
using job_application_management_system_api.Repositories.Services;
using job_application_management_system_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace job_application_management_system_api.Controllers
{
    [ApiController]
    [Route("api/job-application")]
    [EnableCors("AllowSpecificOrigins")]
    public class JobApplicationController : ControllerBase
    {

        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [HttpPost("application")]
        public Result<string> Application([FromForm] JobApplicationDTO jobApplicationDTO)
        {
            return _jobApplicationService.Application(jobApplicationDTO);
        }

        [HttpGet("get-all-job-applications/{jobID}")]
        [Authorize]
        public Result<List<GetJobApplicationDTO>> GetAllJobApplications(int jobID)
        {
            return _jobApplicationService.GetAllJobApplications(jobID);
        }

        [HttpGet("get-job-application/{jobApplicationID}")]
        [Authorize]
        public Result<GetJobApplicationDTO> GetJobApplication(int jobApplicationID)
        {
            return _jobApplicationService.GetJobApplication(jobApplicationID);
        }

    }
}
