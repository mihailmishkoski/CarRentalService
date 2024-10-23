using CarRentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Repository.Interface
{
    public interface IRepository<T> where T : BaseEnum
    {
        IEnumerable<T> GetAll();
        T Get(Guid? id);
        IEnumerable<T> GetByName(string? name);
        T Insert(T entity);
        List<T> InsertMany(List<T> entities);
        T Update(T entity);
        T Delete(T entity);

    }
}
