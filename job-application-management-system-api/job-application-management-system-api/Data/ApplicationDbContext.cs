using job_application_management_system_api.Models;
using Microsoft.EntityFrameworkCore;
using SocialMedia.API.Models;

namespace SocialMedia.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<JobRequirement> JobRequirement { get; set; }
        public DbSet<JobResponsibility> JobResponsibility { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }
        public DbSet<UserSkill> UserSkill { get; set; }

    }
}
