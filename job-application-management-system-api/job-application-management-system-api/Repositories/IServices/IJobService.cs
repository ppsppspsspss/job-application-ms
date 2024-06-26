﻿using job_application_management_system_api.Models;
using job_application_management_system_api.Models.DTOs;
using job_application_management_system_api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace job_application_management_system_api.Repositories.IServices
{
    public interface IJobService
    {
        Result<string> CreateOpening(CreateOpeningDTO createOpeningDTO);
        Result<List<Job>> GetAllJobs(bool showAll);
        Result<JobDTO> GetJob(int jobID);
        Result<string> UpdateStatus(int jobID);
        Result<string> DeleteJob(int jobID);
        Result<string> UpdateJob(int jobID, UpdateOpeningDTO updateOpeningDTO);
    }
}