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
    
    public class CarService : ICarService
    {
        private readonly IRepository<Car> carRepository;

        public CarService(IRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public bool AvailableCheck(Guid ?id)
        {
            return carRepository.Get(id).IsAvailable == true;
        }

        public void CreateNewCar(Car c)
        {
            c.IsAvailable = true;
            carRepository.Insert(c);
        }

        public Car DeleteCar(Guid id)
        {
            Car car = carRepository.Get(id);
            carRepository.Delete(car);
            return car;
        }

        public Car GetCarById(Guid? id)
        {
            return carRepository.Get(id);
        }

        public List<Car> GetCars()
        {
            return carRepository.GetAll().ToList();
        }

        public void SetCarAsAvailable(Guid? id)
        {
            Car car = carRepository.Get(id);
            car.IsAvailable = true;
            carRepository.Update(car);
        }

        public void SetCarAsUnavailable(Guid? id)
        {
            Car car = carRepository.Get(id);
            car.IsAvailable = false;
            carRepository.Update(car);
        }

        public Car UpdateCar(Car id)
        {
            return carRepository.Update(id);
        }
    }
}
