using Core.Base_Classes;
using DAL.Tables;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
   public  class uowBasic : UnitOfWork<uowBasic>
    {
        private HygieneContext  _context;
        public uowBasic(HygieneContext context) : base(context)
        {
            _context = context;

        }

        

        GenericRepository<Client> _ClientRepository = null;
        public GenericRepository<Client> ClientRepository
        {
            get
            {
                _ClientRepository = _ClientRepository ?? new GenericRepository<Client>(_context);
                return _ClientRepository;
            }
        }

       

        List<int> IDsToBeDeleted = new List<int>();

        public    IEnumerable<Client> DownloadAndGetAllClients()
        {
            var data = ClientRepository.GetAllAsQueryable().ToList();
           // var cases = ClientRepository.GetAllAsQueryable().ToList();// temporary solution for ingaz , i should do new migratiton nto bind cases , casetypes
           return data;
        }

        public Client DownloadAndGetClient(int id)
        {
           return ClientRepository.GetAllAsQueryable().FirstOrDefault(ct => ct.Id == id);
        }


        public IEnumerable<Client> GetAllDownloadedClients()
        {
            var data = ClientRepository.GetDownloadedList();
            return data;
        }
        public Client GetExistingClient(int id)
        {
            return ClientRepository.GetDownloadedList().FirstOrDefault(ct => ct.Id == id);
        }
        public   override void  Save()
        {
           try
            {
                 base.Save();                
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async System.Threading.Tasks.Task UpdateClients(List<Client> cts)
        {
            IEnumerable<Client> OldClients = GetAllDownloadedClients();
            cts.ForEach(async (ct) => {
                //if ( ct.Code==null || ct.Code.Trim() == "")
               //     ct.Code = ct.Id.ToString();

                if (ct.Id > 0)//old branch need to be updated 
                {
                    if(ct.Changed )
                      await ClientRepository.Update(ct);
                }
                else
                {//new branh need to be added
                    await ClientRepository.Insert(ct);
                }

           

            });
            //IDsToBeDeleted.ForEach(async (oldID) =>
            //{
            //    if (GetExistingCaseType(oldID) != null)
            //        await CaseTypeRepository.Delete(oldID);
            //    else
            //    {
            //    }
            //});
        }
        //public void MarkAsDeleted(int id)
        //{
        //    IDsToBeDeleted.Add(id);
        //}
    }
}
