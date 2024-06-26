using job_application_management_system_api.Models;
using job_application_management_system_api.Repositories.IServices;
using SocialMedia.API.Data;
using job_application_management_system_api.Models.DTOs;
using System.Globalization;
using job_application_management_system_api.Utils;

namespace job_application_management_system_api.Repositories.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public JobService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public Result<string> CreateOpening(CreateOpeningDTO createOpeningDTO)
        {
            try
            {
                if (createOpeningDTO == null)
                {
                    return new Result<string>(true, new List<string> { "Invalid client request" }, null);
                }

                var _Job = new Job
                {
                    JobTitle = createOpeningDTO.JobTitle,
                    Designation = createOpeningDTO.Designation,
                    JobType = createOpeningDTO.JobType,
                    WorkHourStart = createOpeningDTO.WorkHourStart,
                    WorkHourEnd = createOpeningDTO.WorkHourEnd,
                    Salary = createOpeningDTO.Salary,
                    Negotiable = createOpeningDTO.Negotiable,
                    Description = createOpeningDTO.Description,
                    Phone = createOpeningDTO.Phone,
                    Email = createOpeningDTO.Email,
                    Location = createOpeningDTO.Location,
                    MaxApplicants = createOpeningDTO.MaxApplicants,
                    PostedOn = createOpeningDTO.PostedOn,
                    Deadline = createOpeningDTO.Deadline,
                    Applicants = createOpeningDTO.Applicants,
                    Status = createOpeningDTO.Status
                };

                _db.Job.Add(_Job);
                _db.SaveChanges();

                if (createOpeningDTO.Requirements != null && createOpeningDTO.Requirements.Any())
                {
                    foreach (var requirement in createOpeningDTO.Requirements)
                    {
                        var _jobRequirement = new JobRequirement
                        {
                            JobID = _Job.JobID,
                            Requirement = requirement
                        };
                        _db.JobRequirement.Add(_jobRequirement);
                    }
                    _db.SaveChanges();
                }

                if (createOpeningDTO.Responsibilities != null && createOpeningDTO.Responsibilities.Any())
                {
                    foreach (var responsibility in createOpeningDTO.Responsibilities)
                    {
                        var _jobResponsibility = new JobResponsibility
                        {
                            JobID = _Job.JobID,
                            Responsibility = responsibility
                        };
                        _db.JobResponsibility.Add(_jobResponsibility);
                    }
                    _db.SaveChanges();
                }

                return new Result<string>(false, new List<string> { "Opening created successfully." }, null);
            }
            catch (Exception)
            {
                return new Result<string>(true, new List<string> { "An error occurred while creating the opening." }, null);
            }
        }

        public Result<List<Job>> GetAllJobs()
        {
            try
            {
                var currentDate = DateTime.Today;
                var jobs = _db.Job
                    .Where(job => job.Status == "true" && Convert.ToInt32(job.Applicants) < Convert.ToInt32(job.MaxApplicants))
                    .ToList()
                    .Where(job => ConvertToDateTime(job.Deadline) >= currentDate)
                    .ToList();

                if (jobs.Count > 0)
                {
                    return new Result<List<Job>>(false, new List<string> { "Jobs retrieved successfully" }, jobs);
                }
                else
                {
                    return new Result<List<Job>>(true, new List<string> { "No jobs found" }, null);
                }
            }
            catch (Exception ex)
            {
                return new Result<List<Job>>(true, new List<string> { "Failed to retrieve jobs", ex.Message }, null);
            }
        }


        public Result<Job> GetJob(int jobID)
        {
            var job = _db.Job.FirstOrDefault(job => job.JobID == jobID);

            if (job != null)
            {
                return new Result<Job>(false, new List<string> { "Job found" }, job);
            }
            else
            {
                return new Result<Job>(true, new List<string> { "No job found" }, null);
            }
        }

        public Result<List<JobRequirement>> GetJobRequirements(int jobID)
        {
            var jobRequirements = _db.JobRequirement.Where(requirement => requirement.JobID == jobID).ToList();

            if (jobRequirements.Count > 0)
            {
                return new Result<List<JobRequirement>>(false, new List<string> { "Job requirements found" }, jobRequirements);
            }
            else
            {
                return new Result<List<JobRequirement>>(true, new List<string> { "No job requirements found" }, null);
            }
        }

        public Result<List<JobResponsibility>> GetJobResponsibilities(int jobID)
        {
            var jobResponsibilities = _db.JobResponsibility.Where(responsibility => responsibility.JobID == jobID).ToList();

            if (jobResponsibilities.Count > 0)
            {
                return new Result<List<JobResponsibility>>(false, new List<string> { "Job responsibilities found" }, jobResponsibilities);
            }
            else
            {
                return new Result<List<JobResponsibility>>(true, new List<string> { "No job responsibilities found" }, null);
            }
        }

        public Result<string> UpdateStatus(int jobId)
        {
            var job = _db.Job.FirstOrDefault(job => job.JobID == jobId);

            if (job != null)
            {
                string? status = job.Status;
                if (status != null)
                {
                    job.Status = (status == "true") ? "false" : "true";
                    _db.SaveChanges();
                    return new Result<string>(false, new List<string> { "Status updated successfully." }, job.Status);
                }
                else
                {
                    return new Result<string>(true, new List<string> { "Job status is null." }, null);
                }
            }
            else
            {
                return new Result<string>(true, new List<string> { "Job not found." }, null);
            }
        }

        public Result<string> DeleteJob(int jobID)
        {
            var job = _db.Job.FirstOrDefault(job => job.JobID == jobID);

            if (job != null)
            {
                var jobRequirements = _db.JobRequirement.Where(requirement => requirement.JobID == jobID).ToList();
                var jobResponsibilities = _db.JobResponsibility.Where(responsibility => responsibility.JobID == jobID).ToList();

                if (jobRequirements.Any())
                {
                    _db.JobRequirement.RemoveRange(jobRequirements);
                }

                if (jobResponsibilities.Any())
                {
                    _db.JobResponsibility.RemoveRange(jobResponsibilities);
                }

                _db.Job.Remove(job);
                _db.SaveChanges();

                return new Result<string>(false, new List<string> { "Job deleted successfully." }, null);
            }
            else
            {
                return new Result<string>(true, new List<string> { "Job not found." }, null);
            }
        }

        private DateTime ConvertToDateTime(string? dateString)
        {

            string format = "d MMMM, yyyy";
            DateTime dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

            return dateTime;

        }

        public Result<string> UpdateJob(int jobID, UpdateOpeningDTO updateOpeningDTO)
        {
            try
            {
                var job = _db.Job.FirstOrDefault(job => job.JobID == jobID);

                if (job == null)
                {
                    return new Result<string>(true, new List<string> { "Job not found." }, null);
                }

                job.JobTitle = updateOpeningDTO.JobTitle ?? job.JobTitle;
                job.Designation = updateOpeningDTO.Designation ?? job.Designation;
                job.JobType = updateOpeningDTO.JobType ?? job.JobType;
                job.WorkHourStart = updateOpeningDTO.WorkHourStart ?? job.WorkHourStart;
                job.WorkHourEnd = updateOpeningDTO.WorkHourEnd ?? job.WorkHourEnd;
                job.Salary = updateOpeningDTO.Salary ?? job.Salary;
                job.Negotiable = updateOpeningDTO.Negotiable ?? job.Negotiable;
                job.Description = updateOpeningDTO.Description ?? job.Description;
                job.Phone = updateOpeningDTO.Phone ?? job.Phone;
                job.Email = updateOpeningDTO.Email ?? job.Email;
                job.Location = updateOpeningDTO.Location ?? job.Location;
                job.MaxApplicants = updateOpeningDTO.MaxApplicants ?? job.MaxApplicants;
                job.Deadline = updateOpeningDTO.Deadline ?? job.Deadline;
                job.Status = updateOpeningDTO.Status ?? job.Status;

                if (updateOpeningDTO.Requirements != null)
                {
                    var existingRequirements = _db.JobRequirement.Where(r => r.JobID == jobID).ToList();
                    _db.JobRequirement.RemoveRange(existingRequirements);

                    foreach (var requirement in updateOpeningDTO.Requirements)
                    {
                        var jobRequirement = new JobRequirement
                        {
                            JobID = job.JobID,
                            Requirement = requirement
                        };
                        _db.JobRequirement.Add(jobRequirement);
                    }
                }

                if (updateOpeningDTO.Responsibilities != null)
                {
                    var existingResponsibilities = _db.JobResponsibility.Where(r => r.JobID == jobID).ToList();
                    _db.JobResponsibility.RemoveRange(existingResponsibilities);

                    foreach (var responsibility in updateOpeningDTO.Responsibilities)
                    {
                        var jobResponsibility = new JobResponsibility
                        {
                            JobID = job.JobID,
                            Responsibility = responsibility
                        };
                        _db.JobResponsibility.Add(jobResponsibility);
                    }
                }

                _db.SaveChanges();

                return new Result<string>(false, new List<string> { "Job updated successfully." }, null);
            }
            catch (Exception ex)
            {
                return new Result<string>(true, new List<string> { "An error occurred while updating the job.", ex.Message }, null);
            }
        }


    }
}