using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;


namespace NexusAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<CodingAssessment> CodingAssessments { get; set; }
        public DbSet<CodingQuestion> CodingQuestions { get; set; }
        public DbSet<CodingAssessmentResult> CodingAssessmentResults { get; set; }
        public DbSet<CodingQuestionResult> CodingQuestionResults { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<TestCaseResult> TestCaseResults { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
