using job_application_management_system_api.Models.DTOs;
using job_application_management_system_api.Repositories.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/job")]
[EnableCors("AllowSpecificOrigins")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [AllowAnonymous]
    [HttpPost("job-application")]
    public ActionResult JobApplication([FromBody] JobApplicationDTO jobApplicationDTO)
    {
        try
        {
            _jobService.JobApplication(jobApplicationDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("create-opening")]
    public ActionResult CreateOpening([FromBody] CreateOpeningDTO createOpeningDTO)
    {
        try
        {
            _jobService.CreateOpening(createOpeningDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpGet("get-all-jobs")]
    public ActionResult GetAllJobs()
    {
        try
        {
            var jobs = _jobService.GetAllJobs();
            return Ok(jobs);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("update-status/{jobID}")]
    public ActionResult UpdateStatus(int jobID)
    {
        try
        {
            _jobService.UpdateStatus(jobID);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpGet("get-job/{jobID}")]
    public ActionResult GetJob(int jobID)
    {
        try
        {
            var job = _jobService.GetJob(jobID);
            return Ok(job);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpGet("get-job-requirements/{jobID}")]
    public ActionResult GetJobRequirements(int jobID)
    {
        try
        {
            var jobRequirements = _jobService.GetJobRequirements(jobID);
            return Ok(jobRequirements);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpGet("get-job-responsibilities/{jobID}")]
    public ActionResult GetJobResponsibilities(int jobID)
    {
        try
        {
            var jobResponsibilities = _jobService.GetJobResponsibilities(jobID);
            return Ok(jobResponsibilities);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}