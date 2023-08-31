using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xphyrus.EvaluationAPI;
using Xphyrus.EvaluationAPI.Models;

namespace Xphyrus.EvaluationAPI.Data
{
    public class ApplicatioDbContext : DbContext
    {
        public ApplicatioDbContext(DbContextOptions options) : base(options)
        {
        }
        DbSet<UserSubmissionandSulition> userSubmissionandSulitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
