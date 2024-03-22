using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Xphyrus.NexusAPI.Models;

namespace Xphyrus.NexusAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    

        public DbSet<Nexus> Nexus{ get; set; }
        public DbSet<CodingAssessment>  CodingAssessments { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
