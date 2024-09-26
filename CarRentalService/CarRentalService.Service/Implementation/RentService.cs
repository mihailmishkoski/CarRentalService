using CarRentalService.Domain.Models;
using CarRentalService.Repository.Interface;
using CarRentalService.Service.Interface;
using CarRentalService.Domain.Models.Exceptions;
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
        private readonly IRentParamsService paramsService;
        public RentService(IRepository<Rent> rentRepository, ICarService carService, IRentRepository _rentRepository, IRentParamsService paramsService)
        {
            this.rentRepository = rentRepository;
            this.carService = carService;
            this._rentRepository = _rentRepository;
            this.paramsService = paramsService;
        }
        public Rent CreateNewRent(Rent r, string customerId)
        {
            if (IsRentPeriodOverlapping(r.RentDate, r.ReturnDate, r.CarId))
            {
                throw new CarException("The car is not available in the selected time interval.");
            }
            var rentParams = paramsService.GetRentParams();
            int minDaysForReservation = rentParams.MinimumDaysForRent;
            int rentalDays = (r.ReturnDate - r.RentDate).Days;
            if (rentalDays < minDaysForReservation)
            {
                throw new RentException($"The rental period must be at least {minDaysForReservation} days.");
            }
            r.isActive = true;
            r.CustomerId = customerId;
            var car = carService.GetCarById(r.CarId);
            r.RentAmount = rentalDays * car.PricePerDay;
            try
            {
                return rentRepository.Insert(r);
            }
            catch 
            {
                throw new RentException("An error occurred while saving the rent. Please try again.");
            }
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

        public bool IsRentPeriodOverlapping(DateTime fromDate, DateTime toDate, Guid carId)
        {
            var existingReservations = _rentRepository.GetAllActiveRents(carId);

            
            foreach (var reservation in existingReservations)
            {
                DateTime reservationStartDate = reservation.RentDate;
                DateTime reservationEndDate = reservation.ReturnDate;

                
                if ((fromDate >= reservationStartDate && fromDate <= reservationEndDate) ||
                    (toDate >= reservationStartDate && toDate <= reservationEndDate) ||
                    (reservationStartDate >= fromDate && reservationStartDate <= toDate) ||
                    (reservationEndDate >= fromDate && reservationEndDate <= toDate))
                {
                    
                    return true;
                }
            }
            return false;
        }

        public Rent ReleaseRent(Guid? rentId)
        {
            Rent rent = rentRepository.Get(rentId);
            rent.isActive = false;
            return rentRepository.Update(rent);
        }

        public Rent UpdateRent(Rent rent)
        {
            if (IsRentPeriodOverlapping(rent.RentDate, rent.ReturnDate, rent.CarId))
            {
                throw new CarException("The car is not available in the selected time interval.");
            }
            var rentParams = paramsService.GetRentParams();
            int minDaysForReservation = rentParams.MinimumDaysForRent;
            int rentalDays = (rent.ReturnDate - rent.RentDate).Days;
            if (rentalDays < minDaysForReservation)
            {
                throw new RentException($"The rental period must be at least {minDaysForReservation} days.");
            }
            try
            {
                return rentRepository.Update(rent);
            }
            catch
            { 
                throw new RentException("An error occurred while saving the rent. Please try again.");
            }
        }
    }
}
