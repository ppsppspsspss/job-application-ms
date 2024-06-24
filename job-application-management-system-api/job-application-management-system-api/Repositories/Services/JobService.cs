using job_application_management_system_api.Models;
using job_application_management_system_api.Repositories.IServices;
using SocialMedia.API.Data;
using job_application_management_system_api.Models.DTOs;

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

        public string CreateOpening(CreateOpeningDTO createOpeningDTO)
        {

            var _Job = new Job
            {
                jobTitle = createOpeningDTO.jobTitle,
                designation = createOpeningDTO.designation,
                jobType = createOpeningDTO.jobType,
                
                workHourStart = createOpeningDTO.workHourStart,
                workHourEnd = createOpeningDTO.workHourEnd,
                salary = createOpeningDTO.salary,
                negotiable = createOpeningDTO.negotiable,
                description = createOpeningDTO.description,
                phone = createOpeningDTO.phone,
                email = createOpeningDTO.email,
                location = createOpeningDTO.location,
                maxApplicants = createOpeningDTO.maxApplicants,
                postedOn = createOpeningDTO.postedOn,
                deadline = createOpeningDTO.deadline,
                applicants = createOpeningDTO.applicants,
                status = createOpeningDTO.status
                
            };

            _db.Job.Add(_Job);
            _db.SaveChanges();

            if (createOpeningDTO.Requirements != null && createOpeningDTO.Requirements.Any())
            {
                foreach (var requirement in createOpeningDTO.Requirements)
                {
                    var _jobRequirement = new JobRequirement
                    {
                        jobID = _Job.jobID,
                        requirement = requirement
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
                        jobID = _Job.jobID,
                        responsibility = responsibility
                    };
                    _db.JobResponsibility.Add(_jobResponsibility);
                }
                _db.SaveChanges();
            }

            return "Opening created successfully.";

        }

        public string JobApplication(JobApplicationDTO jobApplicationDTO)
        {

            var _jobApplication = new JobApplication
            {
                jobID = jobApplicationDTO.jobID,
                firstName = jobApplicationDTO.firstName,
                lastName = jobApplicationDTO.lastName,
                fathersName = jobApplicationDTO.fathersName,
                mothersName = jobApplicationDTO.mothersName,
                phone = jobApplicationDTO.phone,
                email = jobApplicationDTO.email,
                currentAddress = jobApplicationDTO.currentAddress,
                permanentAddress = jobApplicationDTO.permanentAddress,
                bscStatus = jobApplicationDTO.bscStatus,
                bscAdmissionDate = jobApplicationDTO.bscAdmissionDate,
                bscAIUB = jobApplicationDTO.bscAIUB,
                bscAIUBID = jobApplicationDTO.bscAIUBID,
                bscUniversity = jobApplicationDTO.bscUniversity,
                bscCGPA = jobApplicationDTO.bscCGPA,
                bscGraduate = jobApplicationDTO.bscGraduate,
                bscGraduationDate = jobApplicationDTO.bscGraduationDate,
                mscStatus = jobApplicationDTO.mscStatus,
                mscAdmissionDate = jobApplicationDTO.mscAdmissionDate,
                mscAIUB = jobApplicationDTO.mscAIUB,
                mscAIUBID = jobApplicationDTO.mscAIUBID,
                mscUniversity = jobApplicationDTO.mscUniversity,
                mscCGPA = jobApplicationDTO.mscCGPA,
                mscGraduate = jobApplicationDTO.mscGraduate,
                mscGraduationDate = jobApplicationDTO.mscGraduationDate,
                cv = jobApplicationDTO.cv
            };

            _db.JobApplication.Add(_jobApplication);
            _db.SaveChanges();

            if (jobApplicationDTO.Skills != null && jobApplicationDTO.Skills.Any())
            {
                foreach (var skill in jobApplicationDTO.Skills)
                {
                    var _userSkill = new UserSkill
                    {
                        jobApplicationID = _jobApplication.jobApplicationID,
                        skill = skill
                    };
                    _db.UserSkill.Add(_userSkill);
                }
                _db.SaveChanges();
            }

            this.IncrementApplicantsCount(jobApplicationDTO.jobID);

            return "Job application submitted successfully.";

        }

        public List<Job> GetAllJobs()
        {

            var jobs = _db.Job.ToList();

            if (jobs.Count > 0) return jobs;
            else throw new Exception("No jobs found");

        }

        public Job GetJob(int jobID)
        {
            var job = _db.Job.FirstOrDefault(job => job.jobID == jobID);

            if (job != null) return job;
            else throw new Exception("No job found");

        }

        public List<JobRequirement> GetJobRequirements(int jobID)
        {
            var jobRequirements = _db.JobRequirement.Where(requirement => requirement.jobID == jobID).ToList();

            if (jobRequirements != null) return jobRequirements;
            else throw new Exception("No job requirements found");

        }

        public List<JobResponsibility> GetJobResponsibilities(int jobID)
        {
            var jobResponsibilities = _db.JobResponsibility.Where(responsibility => responsibility.jobID == jobID).ToList();

            if (jobResponsibilities != null) return jobResponsibilities;
            else throw new Exception("No job responsibilities found");

        }

        public void UpdateStatus(int jobId)
        {
            var job = _db.Job.FirstOrDefault(job => job.jobID == jobId);

            if (job != null)
            {
                string? status = job.status;
                if (status != null)
                {
                    if (status == "true") job.status = "false";
                    else job.status = "true";
                }
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("Job not found.");
            }
        }

        public void IncrementApplicantsCount(int jobId)
        {
            var job = _db.Job.FirstOrDefault(job => job.jobID == jobId);

            if (job != null)
            {
                if (!string.IsNullOrEmpty(job.applicants))
                {
                    int currentApplicants = int.Parse(job.applicants);
                    currentApplicants++;
                    job.applicants = currentApplicants.ToString();
                }
                else
                {
                    job.applicants = "1";
                }
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("Job not found.");
            }
        }

    }
}