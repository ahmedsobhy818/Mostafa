using Business.BusinessObjects;
using Core.Abstract_Classes;
using Core.Base_Classes;
using Core.Interfaces;
using Core.Misc;
using DAL.Tables;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Business.ServiceLayers
{
   public  class slBasic : ServiceLayer<slBasic>
    {
        uowBasic _uow = null;
        public   slBasic(IUnitOfWork<uowBasic> uow)
        {
            UnitOfWork = uow as uowBasic;
            _uow = UnitOfWork as uowBasic;
        }
        public  override void  Save()
        {
            //some custom actions for saving in this service
            try
            {
                if (_AllClients  .isValidData)
                    base.Save();
                else
                    throw new Exception(_AllClients  .Errors);

                
            }
            catch(Exception ex)
            {
                throw ex;  
            }
        }



        //web only
        public doClient GetClient(int id)
        {
            if (_AllClients  == null)
            {
                _AllClients = new MyList<doClient>();
                _AllClients.OnDelete += _AllClients_OnDelete;
            }
            doClient obj= _AllClients.FirstOrDefault(c => c.Id == id);

            if (obj == null)
            {
                obj = DownloadAndGetClient(id);
               // if (obj!=null)
                 //  _AllCaseTypes.Add(obj);
            }
            return obj;

        }
        //web only
        public doClient DownloadAndGetClient(int id)
        {
            Client ClientTable = _uow.DownloadAndGetClient (id);
            if (ClientTable == null)
                return null;

            doClient obj = new doClient {
                Id = ClientTable.Id,
              ClientName = ClientTable.ClientName,
              Responsible = ClientTable.Responsible,
              Fax=ClientTable.Fax,  
              Mail=ClientTable.Mail,
              Siret=ClientTable.Siret,  
              Address=ClientTable.Address,
              Ape=ClientTable.Ape,
              Photo=ClientTable.Photo,
              Tel=ClientTable.Tel,                
              
              ServiceLayer = this

            };

            if(obj!=null)
              _AllClients .Add(obj);

            return obj;
        }
        //web only
        public doClient CreateClient(string tel, string address, string ape,
            string clientName, string fax, string mail,
            string responsible, string  photo, string siret)
        {
            var obj = new doClient { 
                
                Tel=tel,
                Address=address,
                Ape=ape,
                ClientName=clientName,
                Fax=fax,
                Mail=mail,
                Responsible=responsible,
                Photo=photo,
                Siret=siret,
                ServiceLayer=this};

#if MVC_CORE
            if (_AllCaseTypes == null)
            {
                _AllCaseTypes = new MyList<doCaseType>();
                _AllCaseTypes.OnDelete += _AllCaseTypes_OnDelete;
            }
#endif
            _AllClients .Add(obj);
            return obj;
        }

        //web only
        public void RemoveLastClient()
        {
            _AllClients.RemoveAt(_AllClients.Count - 1);
        }

        public void DeleteClient(int id)
        {
            doClient obj = _AllClients.FirstOrDefault(ct => ct.Id == id);
            if (obj == null)
            {
                throw new Exception("هذا العنصر غير متاح");
            }
            int index = _AllClients.IndexOf(obj);
            if (obj.canDelete)
            {
                _AllClients.RemoveAt(index);//.RemoveAll(ct => ct.Id == id);
                SaveClients();
            }
            else
            {
                throw new Exception("لا يمكن حذف هذا العنصر");
            }

        }

        MyList<doClient> _AllClients = null;
        public bool ForceReload = false;
        public MyList<doClient> AllClients()
        {


            if (_AllClients == null || ForceReload )
            {
                ForceReload = false;
                var clients =  _uow.DownloadAndGetAllClients();
                

                MyList<doClient> mylist = new MyList<doClient>();
                clients.ToList().ForEach(ct =>
                {
                    mylist.Add(new doClient 
                    {
                        Id = ct.Id,
                        Address = ct.Address,
                        Ape = ct.Ape,
                        ClientName = ct.ClientName,
                        Fax = ct.Fax,
                        Mail = ct.Mail,
                        Responsible = ct.Responsible,
                        Siret=ct.Siret,
                        Tel=ct.Tel,
                        Photo=ct.Photo,
                        ServiceLayer = this

                    });
                });
                _AllClients = mylist;
                _AllClients.OnDelete += _AllClients_OnDelete;

            }
            return _AllClients;
        }

        //internal List<doClient> GetCasesForType(int id)
        //{

        //    var cases = _uow.GetAllDownloadedClients();
        //    var casesDomains = cases.Select(c => new doCase {CaseTypeId=c.CaseTypeId  });
        //    var myCases= casesDomains.Where(c => c.CaseTypeId == id).ToList();
        //    return myCases;
        //}

        private void _AllClients_OnDelete(MyList<doClient> list, doClient item)
        {
            //if (item.isOld)
            //{
            //    _uow.MarkAsDeleted(item.Id);
            //}
        }

        public  void SaveClients()
        {
            var cts = _AllClients .Where(ct => ct.isOld && ct.isValid ).Select((ct) => {
                var client = _uow.GetExistingClient(ct.Id);
                string str1 = client.ToString();
                client.Id = ct.Id;
                client.Address = ct.Address;
                client.Ape = ct.Ape;
                client.ClientName = ct.ClientName;
                client.Fax = ct.Fax;
                client.Mail = ct.Mail;
                client.Responsible = ct.Responsible;    
                client.Siret = ct.Siret;
                client.Tel = ct.Tel;
                client.Photo = ct.Photo;
                
                client.BussinesObjet = ct;
                string str2 = client.ToString();
                client.Changed = (str1 != str2);
                return client;
            }).ToList();

            _AllClients.Where(ct => ct.isNew && ct.isValid ).ToList().ForEach(ct => {
                cts.Add(new Client
                {
                    Id = 0,
                   Address = ct.Address,
                   Photo = ct.Photo,
                   Ape = ct.Ape,
                   Siret = ct.Siret,
                   Mail = ct.Mail,
                   Fax = ct.Fax,
                   ClientName = ct.ClientName,
                   Responsible = ct.Responsible,
                   Tel = ct.Tel,    
                    BussinesObjet = ct

                });
            });
            //
            //cts.ForEach( (ct) =>
            //{
            //    if (ct.Code == null || ct.Code.Trim() == "")
            //        ct.Code = ct.Id.ToString();
            //});
                //
             _uow.UpdateClients(cts);
             Save();
            //_AllCaseTypes.ForEach(ct => {
            //    if (ct.Code == null ||ct.Code.Trim()==""|| ct.Code=="-1")
            //        ct.Code = ct.Id.ToString();
            //});

        }

      

        //#if DESKTOP
        //public BindingList<doCaseType>  AllCaseTypesBinding
        //{
        //    get
        //    {
        //        if (_AllCaseTypes == null)
        //        {
        //            return null;
        //        }
        //        //  return (this as AbstractServiceLayer).GetBindingList<doCaseType>(_AllCaseTypes);                   
        //        return new BindingList<doCaseType>(_AllCaseTypes);

        //    }
        //    set
        //    {
        //        _AllCaseTypes = value.ToList();// as List<BankBranchObject >;
        //    }
        //}
        //#endif

    }
}
