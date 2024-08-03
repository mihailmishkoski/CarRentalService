using CarRentalService.Domain.Models;
using CarRentalService.Domain.Models.Identity;
using CarRentalService.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Customer> entities;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<Customer>();
        }
        public Customer GetCustomerById(string id)
        {
            return entities.First(c => c.Id == id);
        }
    }
}
