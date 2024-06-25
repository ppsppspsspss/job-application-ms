using job_application_management_system_api.Models.DTOs;
using job_application_management_system_api.Repositories.IServices;
using job_application_management_system_api.Repositories.Services;
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

        [AllowAnonymous]
        [HttpPost("job-application")]
        public ActionResult JobApplication([FromBody] JobApplicationDTO jobApplicationDTO)
        {
            try
            {
                _jobApplicationService.JobApplication(jobApplicationDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [HttpGet("get-all-job-applications/{jobID}")]
        public ActionResult GetAllJobApplications(int jobID)
        {
            try
            {
                var jobApplications = _jobApplicationService.GetAllJobApplications(jobID);
                return Ok(jobApplications);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-job-application/{jobApplicationID}")]
        public ActionResult GetJobApplication(int jobApplicationID)
        {
            try
            {
                var jobApplication = _jobApplicationService.GetJobApplication(jobApplicationID);
                return Ok(jobApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
