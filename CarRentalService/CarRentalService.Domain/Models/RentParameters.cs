using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models
{
    public class RentParameters : BaseEnum
    {
        public int MinimumRentDays { get; set; } = 5;
        public int? Discount { get; set; }


    }
}
