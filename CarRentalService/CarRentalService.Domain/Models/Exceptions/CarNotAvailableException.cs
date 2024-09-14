﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models.Exceptions
{
    public class CarNotAvailableException : Exception
    {

        public CarNotAvailableException(string message) : base(message)
        {
        }
    }
}
