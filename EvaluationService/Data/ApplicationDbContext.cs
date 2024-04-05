using EvaluationService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace EvaluationService.Data
{
    public class ApplicatioDbContext : DbContext
    {
        public ApplicatioDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserSubmissionandSulition> userSubmissionandSulitions { get; set; }
        public DbSet<SubmissionRequest> submissionRequests { get; set; }
        public DbSet<CodingAssessmentResult> CodingAssessmentResult { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
