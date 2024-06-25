using job_application_management_system_api.Models;
using job_application_management_system_api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace job_application_management_system_api.Repositories.IServices
{
    public interface IJobService
    {
        List<Job> GetAllJobs();
        Job GetJob(int jobID);
        List<JobRequirement> GetJobRequirements(int jobID);
        List<JobResponsibility> GetJobResponsibilities(int jobID);
        string JobApplication(JobApplicationDTO jobApplicationDTO);
        string CreateOpening(CreateOpeningDTO createOpeningDTO);
        void UpdateStatus(int jobID);
        string DeleteJob(int jobID);
        string UpdateJob(int jobID, UpdateOpeningDTO updateOpeningDTO);
    }
}