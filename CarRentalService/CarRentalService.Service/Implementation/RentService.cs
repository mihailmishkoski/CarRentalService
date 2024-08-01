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
    public class RentService : IRentService
    {
        private readonly IRepository<Rent> rentRepository;
        private readonly IRentRepository _rentRepository;
        private readonly ICarService carService;
        public RentService(IRepository<Rent> rentRepository, ICarService carService, IRentRepository _rentRepository)
        {
            this.rentRepository = rentRepository;
            this.carService = carService;
            this._rentRepository = _rentRepository;
        }
        public void CreateNewRent(Rent r, string customerId)
        {
            r.CustomerId = customerId;
            rentRepository.Insert(r);
        }

        public Rent DeleteRent(Guid ?id)
        {
            Rent rent = _rentRepository.GetDetailsForRent(id);
            rentRepository.Delete(rent);
            return rent;
        }

        public Rent GetRentById(Guid id)
        {
            return _rentRepository.GetDetailsForRent(id);
        }

        public List<Rent> GetRents()
        {
            return _rentRepository.GetAllRents();
        }

        public Rent UpdateRent(Rent rent)
        {
            return rentRepository.Update(rent);
        }
    }
}
