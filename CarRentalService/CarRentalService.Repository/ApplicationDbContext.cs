using CarRentalService.Domain.Models;
using CarRentalService.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarRentalService.Repository
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
        public virtual DbSet<Return> Returns { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }


        public virtual DbSet<RentParams> RentParam { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<RentParams>().HasData(new RentParams
            {
                Id = Guid.NewGuid(),
                MinimumDaysForRent = 1, 
                DiscountPercentage = 5.0m, 
                AdditionalFees = 10.0m 
            });
        }
    }
}
