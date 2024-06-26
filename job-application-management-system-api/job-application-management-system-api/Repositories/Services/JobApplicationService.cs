using job_application_management_system_api.Models;
using job_application_management_system_api.Models.DTOs;
using job_application_management_system_api.Repositories.IServices;
using job_application_management_system_api.Utils;
using Microsoft.AspNetCore.Builder;
using SocialMedia.API.Data;
using System.Text.RegularExpressions;

namespace job_application_management_system_api.Repositories.Services
{
    public class JobApplicationService : IJobApplicationService
    {

        private readonly ApplicationDbContext _db;

        public JobApplicationService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
        }

        public Result<string> Application(JobApplicationDTO jobApplicationDTO)
        {
            try
            {
                var errors = new List<string>();

                if (string.IsNullOrEmpty(jobApplicationDTO.FirstName)) errors.Add("First name field is required.");
                if (string.IsNullOrEmpty(jobApplicationDTO.LastName)) errors.Add("Last name field is required.");
                if (string.IsNullOrEmpty(jobApplicationDTO.FathersName)) errors.Add("Father's name field is required.");
                if (string.IsNullOrEmpty(jobApplicationDTO.MothersName)) errors.Add("Mother's name field is required.");
                if (string.IsNullOrEmpty(jobApplicationDTO.Phone)) errors.Add("Phone number field is required.");
                else if (!Regex.IsMatch(jobApplicationDTO.Phone, @"^(01)\d{9}$")) errors.Add("Invalid phone number format.");
                if (string.IsNullOrEmpty(jobApplicationDTO.Email)) errors.Add("Email address field is required.");
                else if (_db.JobApplication.Any(app => app.Email == jobApplicationDTO.Email)) errors.Add("An application with this email address already exists.");
                else if (!IsValidEmailFormat(jobApplicationDTO.Email)) errors.Add("Invalid email address format.");
                if (string.IsNullOrEmpty(jobApplicationDTO.CurrentAddress)) errors.Add("Current address field is required.");
                if (string.IsNullOrEmpty(jobApplicationDTO.PermanentAddress)) errors.Add("Permanent address field is required.");
                if (jobApplicationDTO.BscStatus == "true")
                {
                    if (jobApplicationDTO.BscAIUB == "true" && string.IsNullOrEmpty(jobApplicationDTO.BscAIUBID)) errors.Add("AIUB ID field is required.");
                    if (string.IsNullOrEmpty(jobApplicationDTO.BscUniversity)) errors.Add("University field is required.");
                    if (Convert.ToDouble(jobApplicationDTO.BscCGPA) < 0.00 || Convert.ToDouble(jobApplicationDTO.BscCGPA) > 4.00) errors.Add("Invalid CGPA.");
                    if (jobApplicationDTO.BscGraduate == "true" && jobApplicationDTO.BscGraduationDate == null) errors.Add("Graduation date field is required.");
                    if (jobApplicationDTO.BscGraduate == "false" && jobApplicationDTO.BscAdmissionDate == null) errors.Add("Admission date field is required.");

                }
                else
                {
                    errors.Add("Cannot apply without BSc information.");
                }

                if (jobApplicationDTO.MscStatus == "true")
                {
                    if (jobApplicationDTO.MscAIUB == "true" && string.IsNullOrEmpty(jobApplicationDTO.MscAIUBID)) errors.Add("AIUB ID field is required.");
                    if (string.IsNullOrEmpty(jobApplicationDTO.MscUniversity)) errors.Add("University field is required.");
                    if (Convert.ToDouble(jobApplicationDTO.MscCGPA) < 0.00 || Convert.ToDouble(jobApplicationDTO.MscCGPA) > 4.00) errors.Add("CGPA invalid.");
                    if (jobApplicationDTO.MscGraduate == "true" && jobApplicationDTO.MscGraduationDate == null) errors.Add("Graduation date field is required.");
                    if (jobApplicationDTO.MscGraduate == "false" && jobApplicationDTO.MscAdmissionDate == null) errors.Add("Admission date field is required.");

                }

                if (errors.Any())
                {
                    return new Result<string>(true, errors, null);
                }

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

                IncrementApplicantsCount(jobApplicationDTO.JobID);

                return new Result<string>(false, new List<string> { "Job application submitted successfully." }, null);
            }
            catch (Exception ex)
            {
                return new Result<string>(true, new List<string> { ex.Message }, null);
            }
        }

        public Result<List<GetJobApplicationDTO>> GetAllJobApplications(int jobID)
        {
            try
            {
                var jobApplications = _db.JobApplication
                    .Where(jobApplication => jobApplication.JobID == jobID)
                    .Select(jobApplication => new GetJobApplicationDTO
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
                    })
                    .ToList();

                if (jobApplications != null && jobApplications.Any())
                {
                    return new Result<List<GetJobApplicationDTO>>(false, new List<string> { "Job applications found." }, jobApplications);
                }
                else
                {
                    return new Result<List<GetJobApplicationDTO>>(true, new List<string> { "No job applications found." }, null);
                }
            }
            catch (Exception ex)
            {
                return new Result<List<GetJobApplicationDTO>>(true, new List<string> { ex.Message } , null);
            }
        }

        public Result<GetJobApplicationDTO> GetJobApplication(int jobApplicationID)
        {
            try
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
                }).FirstOrDefault();

                if (jobApplication != null)
                {
                    return new Result<GetJobApplicationDTO>(false, new List<string> { "Job application found" }, jobApplication);
                }
                else
                {
                    return new Result<GetJobApplicationDTO>(true, new List<string> { "No job application found" }, null);
                }
            }
            catch (Exception ex)
            {
                return new Result<GetJobApplicationDTO>(true, new List<string> { ex.Message }, null);
            }
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
        private bool IsValidEmailFormat(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
