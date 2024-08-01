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

        public ReturnService(IRepository<Return> returnRepository)
        {
            this.returnRepository = returnRepository;
        }

        public Return CreateNewReturn(Return r)
        {
            return returnRepository.Insert(r);
        }

        public Return DeleteReturn(Guid id)
        {
            Return r = returnRepository.Get(id);
            returnRepository.Delete(r);
            return r;
        }

        public Return GetReturnById(Guid? id)
        {
            return returnRepository.Get(id);
        }

        public List<Return> getReturns()
        {
            return returnRepository.GetAll().ToList();
        }

        public Return UpdateReturn(Return r)
        {
            return returnRepository.Update(r);
        }
    }
}
