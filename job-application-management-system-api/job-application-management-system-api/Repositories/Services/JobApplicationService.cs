using job_application_management_system_api.Models;
using job_application_management_system_api.Models.DTOs;
using job_application_management_system_api.Repositories.IServices;
using Microsoft.AspNetCore.Builder;
using SocialMedia.API.Data;

namespace job_application_management_system_api.Repositories.Services
{
    public class JobApplicationService : IJobApplicationService
    {

        private readonly ApplicationDbContext _db;

        public JobApplicationService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
        }

        public string JobApplication(JobApplicationDTO jobApplicationDTO)
        {

            var _jobApplication = new JobApplication
            {
                JobID = jobApplicationDTO.JobID,
                FirstName = jobApplicationDTO.FirstName,
                LastName = jobApplicationDTO.LastName,
                FathersName = jobApplicationDTO.FathersName,
                MothersName = jobApplicationDTO.MothersName,
                Phone = jobApplicationDTO.Phone,
                Email = jobApplicationDTO.Email,
                CurrentAddress = jobApplicationDTO.CurrentAddress,
                PermanentAddress = jobApplicationDTO.PermanentAddress,
                BscStatus = jobApplicationDTO.BscStatus,
                BscAdmissionDate = jobApplicationDTO.BscAdmissionDate,
                BscAIUB = jobApplicationDTO.BscAIUB,
                BscAIUBID = jobApplicationDTO.BscAIUBID,
                BscUniversity = jobApplicationDTO.BscUniversity,
                BscCGPA = jobApplicationDTO.BscCGPA,
                BscGraduate = jobApplicationDTO.BscGraduate,
                BscGraduationDate = jobApplicationDTO.BscGraduationDate,
                MscStatus = jobApplicationDTO.MscStatus,
                MscAdmissionDate = jobApplicationDTO.MscAdmissionDate,
                MscAIUB = jobApplicationDTO.MscAIUB,
                MscAIUBID = jobApplicationDTO.MscAIUBID,
                MscUniversity = jobApplicationDTO.MscUniversity,
                MscCGPA = jobApplicationDTO.MscCGPA,
                MscGraduate = jobApplicationDTO.MscGraduate,
                MscGraduationDate = jobApplicationDTO.MscGraduationDate,
                Cv = jobApplicationDTO.Cv
            };

            _db.JobApplication.Add(_jobApplication);
            _db.SaveChanges();

            if (jobApplicationDTO.Skills != null && jobApplicationDTO.Skills.Any())
            {
                foreach (var skill in jobApplicationDTO.Skills)
                {
                    var _userSkill = new UserSkill
                    {
                        JobApplicationID = _jobApplication.JobApplicationID,
                        Skill = skill
                    };
                    _db.UserSkill.Add(_userSkill);
                }
                _db.SaveChanges();
            }

            this.IncrementApplicantsCount(jobApplicationDTO.JobID);

            return "Job application submitted successfully.";

        }

        public List<GetJobApplicationDTO> GetAllJobApplications(int jobID)
        {

            var jobApplications = _db.JobApplication.Where(jobApplication => jobApplication.JobID == jobID).Select(jobApplication => new GetJobApplicationDTO
            {
                JobApplicationID = jobApplication.JobApplicationID,
                JobID = jobApplication.JobID,
                FirstName = jobApplication.FirstName,
                LastName = jobApplication.LastName,
                FathersName = jobApplication.FathersName,
                MothersName = jobApplication.MothersName,
                Phone = jobApplication.Phone,
                Email = jobApplication.Email,
                CurrentAddress = jobApplication.CurrentAddress,
                PermanentAddress = jobApplication.PermanentAddress,
                BscStatus = jobApplication.BscStatus,
                BscAdmissionDate = jobApplication.BscAdmissionDate,
                BscAIUB = jobApplication.BscAIUB,
                BscAIUBID = jobApplication.BscAIUBID,
                BscUniversity = jobApplication.BscUniversity,
                BscCGPA = jobApplication.BscCGPA,
                BscGraduate = jobApplication.BscGraduate,
                BscGraduationDate = jobApplication.BscGraduationDate,
                MscStatus = jobApplication.MscStatus,
                MscAdmissionDate = jobApplication.MscAdmissionDate,
                MscAIUB = jobApplication.MscAIUB,
                MscAIUBID = jobApplication.MscAIUBID,
                MscUniversity = jobApplication.MscUniversity,
                MscCGPA = jobApplication.MscCGPA,
                MscGraduate = jobApplication.MscGraduate,
                MscGraduationDate = jobApplication.MscGraduationDate,
                Cv = jobApplication.Cv,
                Skills = _db.UserSkill
                .Where(skill => skill.JobApplicationID == jobApplication.JobApplicationID && skill.Skill != null)
                .Select(skill => skill.Skill!)
                .ToList()
            }).ToList();

            if (jobApplications != null && jobApplications.Any())
                return jobApplications;
            else
                throw new Exception("No job applications found");

        }

        public GetJobApplicationDTO GetJobApplication(int jobApplicationID)
        {
            var jobApplication = _db.JobApplication
        .Where(application => application.JobApplicationID == jobApplicationID)
        .Select(application => new GetJobApplicationDTO
        {
            JobApplicationID = application.JobApplicationID,
            JobID = application.JobID,
            FirstName = application.FirstName,
            LastName = application.LastName,
            FathersName = application.FathersName,
            MothersName = application.MothersName,
            Phone = application.Phone,
            Email = application.Email,
            CurrentAddress = application.CurrentAddress,
            PermanentAddress = application.PermanentAddress,
            BscStatus = application.BscStatus,
            BscAdmissionDate = application.BscAdmissionDate,
            BscAIUB = application.BscAIUB,
            BscAIUBID = application.BscAIUBID,
            BscUniversity = application.BscUniversity,
            BscCGPA = application.BscCGPA,
            BscGraduate = application.BscGraduate,
            BscGraduationDate = application.BscGraduationDate,
            MscStatus = application.MscStatus,
            MscAdmissionDate = application.MscAdmissionDate,
            MscAIUB = application.MscAIUB,
            MscAIUBID = application.MscAIUBID,
            MscUniversity = application.MscUniversity,
            MscCGPA = application.MscCGPA,
            MscGraduate = application.MscGraduate,
            MscGraduationDate = application.MscGraduationDate,
            Cv = application.Cv,
            Skills = _db.UserSkill
                .Where(skill => skill.JobApplicationID == application.JobApplicationID)
                .Select(skill => skill.Skill)
                .Where(skill => skill != null)
                .ToList()!
        })
        .FirstOrDefault();

            if (jobApplication != null) return jobApplication;
            else throw new Exception("No job application found");

        }

        private void IncrementApplicantsCount(int jobId)
        {
            var job = _db.Job.FirstOrDefault(job => job.JobID == jobId);

            if (job != null)
            {
                if (!string.IsNullOrEmpty(job.Applicants))
                {
                    int currentApplicants = int.Parse(job.Applicants);
                    currentApplicants++;
                    job.Applicants = currentApplicants.ToString();
                }
                else
                {
                    job.Applicants = "1";
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
