using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models.Exceptions
{
    public class RentParamsException : Exception
    {
        public RentParamsException(string message) : base(message)
        {

        }
    }
}
