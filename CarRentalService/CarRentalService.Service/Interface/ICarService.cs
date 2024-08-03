using CarRentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Service.Interface
{
    public interface ICarService
    {
        public List<Car> GetCars();
        public Car GetCarById(Guid? id);
        public void CreateNewCar(Car c);
        public Car DeleteCar(Guid id);

        public Car UpdateCar(Car id);

        public bool AvailableCheck(Guid ?id);

        public void SetCarAsUnavailable(Guid ?id);
        public void SetCarAsAvailable(Guid? id);
    }
}
