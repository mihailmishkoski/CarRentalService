using CarRentalService.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Repository.Interface
{
    public interface IUserRepository
    {
        Customer GetCustomerById(string id);
    }
}
