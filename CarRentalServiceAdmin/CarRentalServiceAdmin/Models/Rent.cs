using CarRentalService.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models
{
    public class Rent : BaseEnum
    {
        public Guid CarId { get; set; }
        public string ?CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentAmount { get; set; }
        public Car ?Car { get; set; }
        public Customer ?Customer { get; set; }
        public Return ?Return { get; set; }

        public bool ?isActive {  get; set; }
    }
}
