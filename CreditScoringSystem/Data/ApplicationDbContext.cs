using CreditScoringSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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