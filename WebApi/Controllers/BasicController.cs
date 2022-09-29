using Business.BusinessObjects;
using Business.ServiceLayers;
using Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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

        [Route("api/queries/AllClients")]
        public List<dtoClient> GetAllClients()
        {
            var list = _sl.AllClients();
            return list.Select(c => new dtoClient
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
                
            }).ToList();
        }
    }
}
