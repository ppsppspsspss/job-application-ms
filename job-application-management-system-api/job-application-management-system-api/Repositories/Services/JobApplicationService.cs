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

        public List<GetJobApplicationDTO> GetAllJobApplications(int jobID)
        {

            var jobApplications = _db.JobApplication.Where(jobApplication => jobApplication.jobID == jobID).Select(jobApplication => new GetJobApplicationDTO
            {
                jobApplicationID = jobApplication.jobApplicationID,
                jobID = jobApplication.jobID,
                firstName = jobApplication.firstName,
                lastName = jobApplication.lastName,
                fathersName = jobApplication.fathersName,
                mothersName = jobApplication.mothersName,
                phone = jobApplication.phone,
                email = jobApplication.email,
                currentAddress = jobApplication.currentAddress,
                permanentAddress = jobApplication.permanentAddress,
                bscStatus = jobApplication.bscStatus,
                bscAdmissionDate = jobApplication.bscAdmissionDate,
                bscAIUB = jobApplication.bscAIUB,
                bscAIUBID = jobApplication.bscAIUBID,
                bscUniversity = jobApplication.bscUniversity,
                bscCGPA = jobApplication.bscCGPA,
                bscGraduate = jobApplication.bscGraduate,
                bscGraduationDate = jobApplication.bscGraduationDate,
                mscStatus = jobApplication.mscStatus,
                mscAdmissionDate = jobApplication.mscAdmissionDate,
                mscAIUB = jobApplication.mscAIUB,
                mscAIUBID = jobApplication.mscAIUBID,
                mscUniversity = jobApplication.mscUniversity,
                mscCGPA = jobApplication.mscCGPA,
                mscGraduate = jobApplication.mscGraduate,
                mscGraduationDate = jobApplication.mscGraduationDate,
                cv = jobApplication.cv,
                skills = _db.UserSkill
                .Where(skill => skill.jobApplicationID == jobApplication.jobApplicationID && skill.skill != null)
                .Select(skill => skill.skill!)
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
        .Where(application => application.jobApplicationID == jobApplicationID)
        .Select(application => new GetJobApplicationDTO
        {
            jobApplicationID = application.jobApplicationID,
            jobID = application.jobID,
            firstName = application.firstName,
            lastName = application.lastName,
            fathersName = application.fathersName,
            mothersName = application.mothersName,
            phone = application.phone,
            email = application.email,
            currentAddress = application.currentAddress,
            permanentAddress = application.permanentAddress,
            bscStatus = application.bscStatus,
            bscAdmissionDate = application.bscAdmissionDate,
            bscAIUB = application.bscAIUB,
            bscAIUBID = application.bscAIUBID,
            bscUniversity = application.bscUniversity,
            bscCGPA = application.bscCGPA,
            bscGraduate = application.bscGraduate,
            bscGraduationDate = application.bscGraduationDate,
            mscStatus = application.mscStatus,
            mscAdmissionDate = application.mscAdmissionDate,
            mscAIUB = application.mscAIUB,
            mscAIUBID = application.mscAIUBID,
            mscUniversity = application.mscUniversity,
            mscCGPA = application.mscCGPA,
            mscGraduate = application.mscGraduate,
            mscGraduationDate = application.mscGraduationDate,
            cv = application.cv,
            skills = _db.UserSkill
                .Where(skill => skill.jobApplicationID == application.jobApplicationID)
                .Select(skill => skill.skill)
                .Where(skill => skill != null)
                .ToList()!
        })
        .FirstOrDefault();

            if (jobApplication != null) return jobApplication;
            else throw new Exception("No job application found");

        }

        public string HasApplied(int jobID) {

            var application = _db.JobApplication.FirstOrDefault(application => application.jobID == jobID);
            if (application != null) return "true";
            else return "false";

        }

    }
}
