using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
       Task<IEnumerable<T>> GetAll();
        IQueryable<T> GetAllAsQueryable();
        Task<T> GetById(object id);
        Task Insert(T entity);
        Task Update( T entity);
        Task Delete(object id);
    }
}
