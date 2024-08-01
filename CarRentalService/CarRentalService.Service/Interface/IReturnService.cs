using CarRentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Service.Interface
{
    public interface IReturnService
    {
        public List<Return> getReturns();
        public Return GetReturnById(Guid? id);
        public Return CreateNewReturn(Return r);
        public Return DeleteReturn(Guid id);

        public Return UpdateReturn(Return r);
    }
}
