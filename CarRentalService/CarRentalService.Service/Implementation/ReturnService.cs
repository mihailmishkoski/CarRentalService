using CarRentalService.Domain.Models;
using CarRentalService.Repository.Interface;
using CarRentalService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Service.Implementation
{
    public class ReturnService : IReturnService
    {
        private readonly IRepository<Return> returnRepository;
        private readonly IUserRepository userRepository;
        private readonly IRentService rentService;

        public ReturnService(IRepository<Return> returnRepository, IRentService rentService, IUserRepository userRepository)
        {
            this.returnRepository = returnRepository;
            this.rentService = rentService;
            this.userRepository = userRepository;
        }

        public Return CreateNewReturn(Return r)
        {
            var rent = rentService.ReleaseRent(r.RentId);
            if(DateTime.Now < rent.RentDate)
            {
                r.TotalPrice = rent.RentAmount;
            }
            else
            {
                int totalDays = (DateTime.Now.AddDays(1) - rent.RentDate).Days;
                r.TotalPrice = totalDays * rent.RentAmount + r.LateFee;
            }
            return returnRepository.Insert(r);
        }

        public Return DeleteReturn(Guid id)
        {
            Return r = returnRepository.Get(id);
            returnRepository.Delete(r);
            return r;
        }

        public List<Return> GetMyReturns(string userId)
        {
            return userRepository.GetMyReturns(userId);
        }

        public Return GetReturnById(Guid? id)
        {
            return returnRepository.Get(id);
        }

        public List<Return> GetReturns()
        {
            return returnRepository.GetAll().ToList();
        }

        public Return UpdateReturn(Return r)
        {
            return returnRepository.Update(r);
        }
    }
}
