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
    public class RentParamRepository : IRentParamRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<RentParams> entities;

        public RentParamRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<RentParams>();
        }
        public RentParams GetRentParams()
        {
            return entities.FirstOrDefault();
        }
    }
}
