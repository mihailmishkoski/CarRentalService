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
            if (carService.AvailableCheck(r.CarId))
            {
                carService.SetCarAsUnavailable(r.CarId);
                r.CustomerId = customerId;
                rentRepository.Insert(r);
            }
            //else return exception
        }

        /// <summary>
        /// Deletes a rent and sets the associated car as available.This is an atomic operation, 
        /// so changes to the Car and Rent are made within a try/catch block to ensure consistency.
        /// </summary>
        /// <param name="id">The unique identifier of the rent to be deleted.</param>
        /// <returns>Deleted rent</returns>
        public Rent DeleteRent(Guid ?id)
        {
            Rent rent = _rentRepository.GetDetailsForRent(id);
            try 
            {
                rentRepository.Delete(rent);
                carService.SetCarAsAvailable(rent.CarId);
            }
            catch 
            {
                //No rent exception
            }
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
