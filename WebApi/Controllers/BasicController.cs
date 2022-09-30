using Business.BusinessObjects;
using Business.ServiceLayers;
using Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class BasicController : ControllerBase
    {
        slBasic _sl;
        public BasicController(IServiceLayer<slBasic> sl)
        {
            _sl = sl as slBasic;
        }
        #region client
        [Route("api/queries/AllClients")]
        public ActionResult GetAllClients()
        {
            var list = _sl.AllClients();
            return Ok(list.Select(c => new dtoClient
            {
                Id = c.Id,
                Address = c.Address,
                Ape = c.Ape,
                ClientName = c.ClientName,
                Fax = c.Fax,
                Mail= c.Mail,   
                Photo = c.Photo,
                Responsible = c.Responsible,
                Siret  = c.Siret,
                Tel= c.Tel
                
            }));
        }

        [Route("api/queries/GetClient")]
        public ActionResult GetCaseType(int id)
        {
            doClient c = _sl.GetClient (id);
            if (c == null)
                return BadRequest(new { message= "Client Not Exist" });

            return Ok( new dtoClient {
                Id = c.Id,
                Address = c.Address,
                Ape = c.Ape,
                ClientName = c.ClientName,
                Fax = c.Fax,
                Mail = c.Mail,
                Photo = c.Photo,
                Responsible = c.Responsible,
                Siret = c.Siret,
                Tel = c.Tel
            });
        }

        //[HttpPost]
        [Route("api/queries/CreateClient")]
        public ActionResult CreateCaseType(dtoClient data)
        {
            doClient obj = _sl.CreateClient(data.Tel, data.Address, data.Ape, data.ClientName, data.Fax, data.Mail, data.Responsible, data.Photo, data.Siret);
            try
            {
                _sl.SaveClients();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/queries/UpdateClient")]
        public ActionResult UpdateClient(dtoClient _new)
        {

            try
            {
                var obj = _sl.GetClient (_new.Id);
                if (obj == null)
                    return BadRequest(new { message = "Client Not Exist" });

                obj.Address = _new.Address;
                obj.Ape = _new.Ape;
                obj.ClientName = _new.ClientName;
                obj.Fax = _new.Fax;
                obj.Mail  = _new.Mail ;
                obj.Photo = _new.Photo;
                obj.Responsible  = _new.Responsible ;
                obj.Siret  = _new.Siret;
                obj.Tel  = _new.Tel ;



                _sl.SaveClients();

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/queries/DeleteClient")]
        public ActionResult  DeleteCaseType(dynamic data)
        {

            try
            {
                var obj = _sl.GetClient ((int)data.Id);
                if (obj == null)
                    return BadRequest(new { message = "Client Not Exist" });

                try
                {
                    _sl.DeleteClient((int)data.Id);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }


            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        #endregion
    }
}
