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


        public DbSet<CodingAssessmentResult> CodingAssessmentResult { get; set; }
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
