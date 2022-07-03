using CompanyManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Data
{
    public class CompanyManagementDbContext : DbContext
    {
        public CompanyManagementDbContext() : base()
        {
        }

        public CompanyManagementDbContext(DbContextOptions<CompanyManagementDbContext> options):base(options)
        {

        }

        #region Tables
        public DbSet<DbCompany> Companies { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DbCompany>().HasIndex(x=>x.CompanyName).IsUnique(true);
        }
    }
}