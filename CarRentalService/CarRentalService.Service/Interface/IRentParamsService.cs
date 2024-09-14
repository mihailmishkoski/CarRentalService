using CarRentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Service.Interface
{
    public interface IRentParamsService
    {
        public RentParams GetRentParams();

        public RentParams UpdateRentParams(RentParams rp);

        public int CalculateLateFee(Rent rent, DateTime returnDate, decimal additionalFees);
    }
}
