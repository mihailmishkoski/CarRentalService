using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models
{
    public class Car : BaseEnum
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Model { get; set; }

        public DateOnly DateManufactured { get; set; }

        public int KilometersTraveled { get; set; }

        public ColorVehicle Color { get; set; }

        public string LicensePlate { get; set; }

        public ICollection<Rent>? Rents { get; set; }
        public int PricePerDay {  get; set; }
    }
}
