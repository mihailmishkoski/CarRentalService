using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models.Identity
{
    public class Customer : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }


        public ICollection<Rent>? Rents { get; set; }
    }
}
