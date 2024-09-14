using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models
{
    public class RentParams : BaseEnum
    {
        public Guid Id { get; set; }
        public int MinimumDaysForRent {  get; set; }    
        public decimal DiscountPercentage {  get; set; }
        public decimal AdditionalFees { get; set; }

    }
}
