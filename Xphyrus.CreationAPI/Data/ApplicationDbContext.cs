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
        public DbSet<EvliationCase> evliationCases { get; set; }
        public DbSet<MasterCode> MasterCode { get; set; }
        public DbSet<COnstraint> constrainss { get; set; }
        public DbSet<Example> examples { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
