using CarRentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Service.Interface
{
    public interface IRentService
    {
        public List<Rent> GetRents();
        public Rent GetRentById(Guid id);
        public Rent CreateNewRent(Rent r, string customerId);
        public Rent DeleteRent(Guid ?id);

        public Rent UpdateRent(Rent rent);
        Rent ReleaseRent(Guid? rentId);

        bool IsRentPeriodOverlapping(DateTime fromDate, DateTime toDate, Guid carId);
    }
}
