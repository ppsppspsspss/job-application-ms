using job_application_management_system_api.Models;
using job_application_management_system_api.Models.DTOs;
using job_application_management_system_api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace job_application_management_system_api.Repositories.IServices
{
    public interface IJobService
    {
        Result<string> CreateOpening(CreateOpeningDTO createOpeningDTO);
        Result<List<Job>> GetAllJobs();
        Result<Job> GetJob(int jobID);
        Result<List<JobRequirement>> GetJobRequirements(int jobID);
        Result<List<JobResponsibility>> GetJobResponsibilities(int jobID);
        void UpdateStatus(int jobID);
        string DeleteJob(int jobID);
        string UpdateJob(int jobID, UpdateOpeningDTO updateOpeningDTO);
    }
}