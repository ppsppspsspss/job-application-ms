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
    [Authorize]
    public Result<string> CreateOpening([FromBody] CreateOpeningDTO createOpeningDTO)
    {
        return _jobService.CreateOpening(createOpeningDTO);
    }

    [HttpPut("update-job/{jobID}")]
    [Authorize]
    public Result<string> UpdateJob(int jobID, [FromBody] UpdateOpeningDTO updateOpeningDTO)
    {
        return _jobService.UpdateJob(jobID, updateOpeningDTO);
    }

    [HttpGet("get-all-jobs/{showAll}")]
    public Result<List<Job>> GetAllJobs(bool showAll)
    {
        return _jobService.GetAllJobs(showAll);
    }

    [HttpPatch("update-status/{jobID}")]
    [Authorize]
    public Result<string> UpdateStatus(int jobID)
    {
        return _jobService.UpdateStatus(jobID);
    }

    [HttpGet("get-job/{jobID}")]
    public Result<Job> GetJob(int jobID)
    {
        return _jobService.GetJob(jobID);
    }

    [HttpGet("get-job-requirements/{jobID}")]
    public Result<List<JobRequirement>> GetJobRequirements(int jobID)
    {
        return _jobService.GetJobRequirements(jobID);
    }

    [HttpGet("get-job-responsibilities/{jobID}")]
    public Result<List<JobResponsibility>> GetJobResponsibilities(int jobID)
    {
        return _jobService.GetJobResponsibilities(jobID);
    }

    [HttpDelete("delete-job/{jobID}")]
    [Authorize]
    public Result<string> DeleteJob(int jobID)
    {
        return _jobService.DeleteJob(jobID);
    }


}