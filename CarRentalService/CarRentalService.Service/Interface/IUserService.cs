using CarRentalService.Domain.Models;
using CarRentalService.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Service.Interface
{
    public interface IUserService
    {
        Customer GetCustomerById(string id);

        List<Rent> GetMyRents(string id); 
    }
}
