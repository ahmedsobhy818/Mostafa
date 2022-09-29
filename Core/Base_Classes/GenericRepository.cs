using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Base_Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> table = null;
        public GenericRepository(DbContext context)
        {
            _context = context;
            table = context.Set<T>();
        }
        public async Task  Delete(object id)
        {
       
            T existing = await GetById(id);
            // var ent = _context.Entry(existing); //ent.state will equal "Unchanged"
            table.Remove(existing);
            // ent = _context.Entry(existing);  //ent.state will equal "Deleted"
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync(); //downloads all table data
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return table; //pointer to table data
        }

        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id); 
        }

        public async Task Insert(T entity)
        {
            await table.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            //table.Update(entity);  works

            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
     
            
        }

        public IEnumerable<T> GetDownloadedList()
        {
            //get list of already loaded objects of T type
            return _context.ChangeTracker.Entries().Where(obj => obj.Entity is T && obj.State!=EntityState.Deleted ).Select(entry=>entry.Entity as T);
            
        }

       
    }
}
