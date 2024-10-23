using CarRentalService.Domain.Models;
using CarRentalService.Domain.Models.Exceptions;
using CarRentalService.Repository.Interface;
using CarRentalService.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarRentalService.Service.Implementation
{
    
    public class CarService : ICarService
    {
        private readonly IRepository<Car> carRepository;

        public CarService(IRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }


        public void CreateNewCar(Car c)
        {

            if (!IsLicencePlateValid(c.LicensePlate, true))
            {
                throw new CarException("The license plate must be in this format: AB-1234-CD");
            }
            try
            {
                carRepository.Insert(c);
            }
            catch
            {
                throw new CarException("An error occurred while saving the car. Please try again.");
            }
        }


        public Car DeleteCar(Guid id)
        {
            Car car = carRepository.Get(id);
            carRepository.Delete(car);
            return car;
        }

        public IEnumerable<Car> GetByName(string? name)
        {
            return carRepository.GetByName(name);
        }

        public Car GetCarById(Guid? id)
        {
            return carRepository.Get(id);
        }

        public List<Car> GetCars()
        {
            return carRepository.GetAll().ToList();
        }

        public bool IsLicencePlateValid(string licencePlate, bool? isCreate)
        {

            var licensePlateFormat = @"^[A-Z]{2}-\d{4}-[A-Z]{2}$";

            licencePlate = licencePlate.ToUpper();


            if (!Regex.IsMatch(licencePlate, licensePlateFormat))
            {
                return false; 
            }
            if (isCreate == true)
            {
                List<Car> cars = GetCars();
                foreach (Car car in cars)
                {
                    if (car.LicensePlate.Equals(licencePlate))
                    {
                        return false;
                    }
                }
            }
            return true; 
        }


        public Car UpdateCar(Car car)
        {
            if (!IsLicencePlateValid(car.LicensePlate, false))
            {
                throw new CarException("The license plate must be in this format: AB-1234-CD");
            }
            return carRepository.Update(car);
        }
        
    }
}
