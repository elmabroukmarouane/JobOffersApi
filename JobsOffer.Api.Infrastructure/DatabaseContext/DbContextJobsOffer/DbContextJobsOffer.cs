using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.DbContextJobsOffer
{
    public class DbContextJobsOffer : DbContext
    {
        public DbSet<DomainJob> DomainJobs { get; set; }
        public DbSet<Profil> Profils { get; set; }
        public DbSet<WebSite> WebSites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ProfilDomainJob> ProfilDomainJobs { get; set; }
        public DbSet<Favori> Favoris { get; set; }

        public DbContextJobsOffer(DbContextOptions<DbContextJobsOffer> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // START Config for Auto-increment PostgreSQL
            //modelBuilder.UseSerialColumns();
            // END Config for Auto-increment PostgreSQL

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
