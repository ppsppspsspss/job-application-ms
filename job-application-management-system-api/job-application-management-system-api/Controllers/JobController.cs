using job_application_management_system_api.Models;
using job_application_management_system_api.Models.DTOs;
using job_application_management_system_api.Repositories.IServices;
using job_application_management_system_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Models.DTOs;

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

    [HttpPost("create-opening")]
    public Result<string> CreateOpening([FromBody] CreateOpeningDTO createOpeningDTO)
    {
        return _jobService.CreateOpening(createOpeningDTO);
    }

    [HttpPut("update-job/{jobID}")]
    public ActionResult UpdateJob(int jobID, [FromBody] UpdateOpeningDTO updateOpeningDTO)
    {
        try
        {
            _jobService.UpdateJob(jobID, updateOpeningDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpGet("get-all-jobs")]
    public Result<List<Job>> GetAllJobs()
    {
        return _jobService.GetAllJobs();
    }

    [HttpPatch("update-status/{jobID}")]
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
    public Result<Job> GetJob(int jobID)
    {
        return _jobService.GetJob(jobID);
    }

    [AllowAnonymous]
    [HttpGet("get-job-requirements/{jobID}")]
    public Result<List<JobRequirement>> GetJobRequirements(int jobID)
    {
        return _jobService.GetJobRequirements(jobID);
    }

    [AllowAnonymous]
    [HttpGet("get-job-responsibilities/{jobID}")]
    public Result<List<JobResponsibility>> GetJobResponsibilities(int jobID)
    {
        return _jobService.GetJobResponsibilities(jobID);
    }

    [HttpDelete("delete-job/{jobID}")]
    public ActionResult DeleteJob(int jobID)
    {
        try
        {
            _jobService.DeleteJob(jobID);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }



}