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
                if (createOpeningDTO == null) return new Result<string>(true, new List<string> { "Invalid request" }, null);
               
                var validationErrors = new List<string>();

                if (string.IsNullOrWhiteSpace(createOpeningDTO.JobTitle)) validationErrors.Add("Opening title cannot be empty");
                if (string.IsNullOrWhiteSpace(createOpeningDTO.Designation)) validationErrors.Add("Designation cannot be empty");
                if (!string.IsNullOrWhiteSpace(createOpeningDTO.WorkHourStart) &&
                            !string.IsNullOrWhiteSpace(createOpeningDTO.WorkHourEnd) &&
                            DateTime.Parse(createOpeningDTO.WorkHourStart) >= DateTime.Parse(createOpeningDTO.WorkHourEnd))
                {
                    validationErrors.Add("Work hour start cannot be greater than or equal to work hour end");
                }
                if (string.IsNullOrWhiteSpace(createOpeningDTO.Description)) validationErrors.Add("Description cannot be empty");
                if (createOpeningDTO.MaxApplicants == null) validationErrors.Add("Max applicants cannot be empty");
                if (createOpeningDTO.MaxApplicants != null && Convert.ToInt32(createOpeningDTO.MaxApplicants) <= 0) validationErrors.Add("Max applicants must be greater than 0");
                if (createOpeningDTO.Deadline == null) validationErrors.Add("Opening deadline cannot be empty");
                if (createOpeningDTO.Deadline != null && ConvertToDateTime(createOpeningDTO.Deadline) < DateTime.Now) validationErrors.Add("Opening deadline cannot be a past date");

                if (validationErrors.Any())
                {
                    return new Result<string>(true, validationErrors, null);
                }

                Job _Job = new ()
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
            catch (Exception ex)
            {
                return new Result<string>(true, new List<string> { "An error occurred while creating the opening.", ex.Message }, null);
            }
        }


        public Result<List<Job>> GetAllJobs(bool showAll)
        {
            try
            {
                var currentDate = DateTime.Today;
                List<Job> jobs;

                if (showAll) jobs = _db.Job.ToList();
                else
                {
                    jobs = _db.Job
                        .Where(job => job.Status == "true" && Convert.ToInt32(job.Applicants) < Convert.ToInt32(job.MaxApplicants))
                        .ToList()
                        .Where(job => ConvertToDateTime(job.Deadline) >= currentDate)
                        .ToList();
                }

                if (jobs.Count > 0) return new Result<List<Job>>(false, new List<string> { "Jobs retrieved successfully" }, jobs);
                else return new Result<List<Job>>(true, new List<string> { "No jobs found" }, null);

            }

            catch (Exception ex) 
            { 
                return new Result<List<Job>>(true, new List<string> { "Failed to retrieve jobs", ex.Message }, null);
            } 

        }

        public Result<JobDTO> GetJob(int jobID)
        {
            var job = _db.Job.FirstOrDefault(job => job.JobID == jobID);

            if (job != null)
            {
                JobDTO jobDTO = new ()
                {
                    JobID = job.JobID,
                    JobTitle = job.JobTitle,
                    JobType = job.JobType,
                    Applicants = job.Applicants,
                    Deadline = job.Deadline,
                    Description = job.Description,
                    Designation = job.Designation,
                    Email = job.Email,
                    Location = job.Location,
                    MaxApplicants = job.MaxApplicants,
                    Negotiable = job.Negotiable,
                    Phone = job.Phone,
                    PostedOn = job.PostedOn,
                    Requirements = GetJobRequirements(job.JobID),
                    Responsibilities = GetJobResponsibilities(job.JobID),
                    Salary = job.Salary,
                    Status = job.Status,
                    WorkHourEnd = job.WorkHourEnd,
                    WorkHourStart = job.WorkHourStart
                };
                return new Result<JobDTO>(false, new List<string> { "Job found" }, jobDTO);
            }
            else
            {
                return new Result<JobDTO>(true, new List<string> { "No job found" }, null);
            }
        }

        private List<string?> GetJobRequirements(int jobID)
        {
            return _db.JobRequirement.Where(requirement => requirement.JobID == jobID).Select(requirement => requirement.Requirement).ToList();
        }

        public List<string?> GetJobResponsibilities(int jobID)
        {
            return _db.JobResponsibility.Where(responsibility => responsibility.JobID == jobID).Select(responsibility => responsibility.Responsibility).ToList();
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

                if (Convert.ToInt32(job.Applicants) > 0) return new Result<string>(true, new List<string> { $"Job cannot be deleted as it has {job.Applicants} applicants." }, null);

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

            else return new Result<string>(true, new List<string> { "Job not found." }, null);

        }


        private DateTime? ConvertToDateTime(string? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString)) return null;
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

                var validationErrors = new List<string>();

                if (string.IsNullOrWhiteSpace(updateOpeningDTO.JobTitle)) validationErrors.Add("Opening title cannot be empty");
                if (string.IsNullOrWhiteSpace(updateOpeningDTO.Designation)) validationErrors.Add("Designation cannot be empty");
                if (!string.IsNullOrWhiteSpace(updateOpeningDTO.WorkHourStart) &&
                    !string.IsNullOrWhiteSpace(updateOpeningDTO.WorkHourEnd) &&
                    DateTime.Parse(updateOpeningDTO.WorkHourStart) >= DateTime.Parse(updateOpeningDTO.WorkHourEnd))
                {
                    validationErrors.Add("Work hour start cannot be greater than or equal to work hour end");
                }
                if (string.IsNullOrWhiteSpace(updateOpeningDTO.Description)) validationErrors.Add("Description cannot be empty");
                if (updateOpeningDTO.MaxApplicants == null || updateOpeningDTO.MaxApplicants == "0") validationErrors.Add("Max applicants cannot be empty or 0");
                else
                {
                    int currentApplicants = Convert.ToInt32(_db.Job.FirstOrDefault(j => j.JobID == jobID)?.Applicants ?? "0");

                    if (Convert.ToInt32(updateOpeningDTO.MaxApplicants) <= currentApplicants) validationErrors.Add($"Max applicants must be greater than current applicants ({currentApplicants})");
                    else if (Convert.ToInt32(updateOpeningDTO.MaxApplicants) <= 0) validationErrors.Add("Max applicants must be greater than 0");
                }

                if (updateOpeningDTO.Deadline == null) validationErrors.Add("Opening deadline cannot be empty");
                else if (ConvertToDateTime(updateOpeningDTO.Deadline) < DateTime.Now) validationErrors.Add("Opening deadline cannot be a past date");


                if (validationErrors.Any())
                {
                    return new Result<string>(true, validationErrors, null);
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

                var existingRequirements = _db.JobRequirement.Where(r => r.JobID == jobID).ToList();
                _db.JobRequirement.RemoveRange(existingRequirements);

                if (updateOpeningDTO.Requirements != null && updateOpeningDTO.Requirements.Any())
                {
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

                var existingResponsibilities = _db.JobResponsibility.Where(r => r.JobID == jobID).ToList();
                _db.JobResponsibility.RemoveRange(existingResponsibilities);

                if (updateOpeningDTO.Responsibilities != null && updateOpeningDTO.Responsibilities.Any())
                {
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
                return new Result<string>(true, new List<string> { ex.Message }, null);
            }
        }


    }
}