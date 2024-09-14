using CarRentalService.Domain.Models;
using CarRentalService.Repository.Interface;
using CarRentalService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Service.Implementation
{
    public class RentParamsService : IRentParamsService
    {
        private readonly IRentParamRepository _rentParamRepository;
        private readonly IRepository<RentParams> _rentParamRepo;

        public RentParamsService(IRentParamRepository rentParamRepository, IRepository<RentParams> rentParamRepo)
        {
            _rentParamRepository = rentParamRepository;
            _rentParamRepo = rentParamRepo;
        }

        public int CalculateLateFee(Rent rent, DateTime returnDate, decimal additionalFees)
        {
            if (returnDate > rent.ReturnDate)
            {
                var lateDays = (returnDate - rent.ReturnDate).Days;
                return lateDays * (int)additionalFees;
            }
            return 0;
        }

        public Domain.Models.RentParams GetRentParams()
        {
            return _rentParamRepository.GetRentParams();
        }

        public RentParams UpdateRentParams(RentParams rp)
        {
            return _rentParamRepo.Update(rp);
        }
    }
}
