using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models
{
    public class Return : BaseEnum
    {
        public Guid RentId { get; set; }

        public Rent ?Rent { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? LateFee { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
