using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


using Xphyrus.ResultAPI.Models;

namespace Xphyrus.ResultAPI.Data
{
    public class ApplicatioDbContext : DbContext
    {
        public ApplicatioDbContext(DbContextOptions options) : base(options)
        {
        }
    

        public DbSet<Nexus> Nexus{ get; set; }
        public DbSet<CodingAssessment>  CodingAssessments { get; set; }
        public DbSet<TestCase> TestCases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
