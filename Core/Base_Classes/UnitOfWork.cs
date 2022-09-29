using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Abstract_Classes;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Base_Classes
{
    
    
    public class UnitOfWork<T> : AbstractUnitOfWork, IUnitOfWork<T> where T : class
    {
        //private readonly DbContext _context;
        public UnitOfWork(DbContext context)
        {
            _Context = context;
            _Context.SavedChanges += _context_SavedChanges;
            _Context.SavingChanges += _context_SavingChanges;
            _Context.SaveChangesFailed += _context_SaveChangesFailed;
        }

        private void _context_SaveChangesFailed(object sender, SaveChangesFailedEventArgs e)
        {
            string exception = e.Exception.Message;
            
        }

        private void _context_SavingChanges(object sender, SavingChangesEventArgs e)
        {
            
        }

        private void _context_SavedChanges(object sender, SavedChangesEventArgs e)
        {
            
            int count=e.EntitiesSavedCount;
            
        }

        public override void Save()
        {
            //base.Save();
            try
            {

                 _Context.SaveChanges();
                SyncIDs();
            }
            catch (Exception ex)
            {
               // _Context.ChangeTracker.Entries().Where(c => (c.Entity as EntityBase).Id <= 0).ToList().ForEach(e => e.State = EntityState.Detached);
                _Context.ChangeTracker.Entries().Where(c => c.State == EntityState.Added).ToList().ForEach(e => e.State = EntityState.Detached );
                _Context.ChangeTracker.Entries().Where(c=>c.State==EntityState.Deleted).ToList().ForEach(e => e.State = EntityState.Unchanged);
                _Context.ChangeTracker.Entries().Where(c => c.State == EntityState.Modified).ToList().ForEach(e => e.State = EntityState.Unchanged);
                throw ex;
            }
        }

        public  IEnumerable<EntityBase> GetAllDownloadedData()
        {
            var data= _Context.ChangeTracker.Entries().Select(entry => entry.Entity as EntityBase )  ;
            return data;
        }
      
        private void SyncIDs()
        {
            GetAllDownloadedData().ToList().ForEach(entity =>
            {
                if (entity.BussinesObjet != null)
                {
                    if(entity.BussinesObjet.Id<1)
                      entity.BussinesObjet.Id = entity.Id;
                }
            });
        }

    }

    
}
