using CarRentalService.Domain.Models;
using CarRentalService.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Repository.Implementation
{
    public class RentRepository : IRentRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Rent> entities;

        public RentRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Rent>();
        }
        public List<Rent> GetAllRents()
        {
            return entities.Include(r => r.Car).ToList();
        }

        public Rent GetDetailsForRent(Guid ?id)
        {
            return entities.Include(r => r.Customer).Include(r => r.Car).
                Where(r => r.Id == id).FirstOrDefault();
        }
    }
}
