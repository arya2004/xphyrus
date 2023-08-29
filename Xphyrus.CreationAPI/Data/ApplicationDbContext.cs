using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xphyrus.CreationAPI.Models;

namespace Xphyrus.CreationAPI.Data
{
    public class ApplicatioDbContext : DbContext
    {
        public ApplicatioDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Coding> Coding { get; set; }
        public DbSet<EvliationCase> EvliationCases { get; set; }
        public DbSet<MasterCode> MasterCode { get; set; }
        public DbSet<COnstraint> Constrainss { get; set; }
        public DbSet<Example> Examples { get; set; }
        public DbSet<Assesment> Assesments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
