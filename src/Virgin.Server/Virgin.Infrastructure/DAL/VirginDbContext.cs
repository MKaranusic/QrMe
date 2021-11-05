using Microsoft.EntityFrameworkCore;
using Virgin.GenericRepository.DAL;
using Virgin.Infrastructure.Entities;

namespace Virgin.Infrastructure.DAL
{
    public class VirginDbContext : AppDbContext
    {
        public DbSet<CustomerRedirect> CustomerRedirects { get; set; }

        public VirginDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerRedirect>()
                .HasIndex(p => new { p.CustomerId, p.IsActive })
                .IsUnique()
                .HasFilter("[IsActive]=1");

            modelBuilder.Entity<CustomerRedirect>()
                .Property(x=>x.TimesViewed)
                .HasDefaultValue(0);
        }
    }
}