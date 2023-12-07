using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xphyrus.AssesmentAPI.Models;

namespace Xphyrus.AssesmentAPI.Data
{
    public class ApplicatioDbContext : DbContext
    {
        public ApplicatioDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EvaluationCase> EvliationCases { get; set; }
        public DbSet<CodingAssesment> Assesments { get; set; }
        public DbSet<AssesmentAdmins> AssesmentAdmins { get; set; }
        public DbSet<AssesmentParticipant> AssesmentParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
       
        }
    }
}
