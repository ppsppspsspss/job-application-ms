﻿using job_application_management_system_api.Models;
using job_application_management_system_api.Models.DTOs;
using SocialMedia.API.Models.DTOs;

namespace job_application_management_system_api.Repositories.IServices
{
    public interface IJobApplicationService
    {
        string JobApplication(JobApplicationDTO jobApplicationDTO);
        List<GetJobApplicationDTO> GetAllJobApplications(int jobID);
        GetJobApplicationDTO GetJobApplication(int jobApplicationID);
    }
}
