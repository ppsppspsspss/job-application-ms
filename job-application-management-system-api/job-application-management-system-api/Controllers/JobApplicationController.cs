using job_application_management_system_api.Repositories.IServices;
using job_application_management_system_api.Repositories.Services;
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

        [HttpGet("has-applied/{jobID}")]
        public ActionResult HasApplied(int jobID)
        {
            try
            {
                var status = _jobApplicationService.HasApplied(jobID);
                return Ok(status);
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
