﻿using CarRentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Repository.Interface
{
    public interface IRentParamRepository
    {
        RentParams GetRentParams();
    }
}