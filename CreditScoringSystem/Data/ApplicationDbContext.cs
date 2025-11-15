using Microsoft.EntityFrameworkCore;
using CreditScoringSystem.Models;

namespace CreditScoringSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CreditApplication> CreditApplications { get; set; }
    }
}