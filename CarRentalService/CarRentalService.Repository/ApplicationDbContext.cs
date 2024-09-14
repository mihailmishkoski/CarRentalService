using CarRentalService.Domain.Models;
using CarRentalService.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
<<<<<<< HEAD

        public virtual DbSet<RentParams> RentParam { get; set; }
=======
        public virtual DbSet<RentParameters> RentParameters{ get; set; }
>>>>>>> 8ce4a619920af9a110912a90112c5c8dd936636a
    }
}
