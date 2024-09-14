using CarRentalService.Domain.Models;
using CarRentalService.Domain.Models.Identity;
using CarRentalService.Repository.Interface;
using CarRentalService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public Customer GetCustomerById(string id)
        {
            return _userRepository.GetCustomerById(id);
        }

        public List<Rent> GetMyRents(string id)
        {
            return _userRepository.GetMyRents(id);
        }
    }
}
