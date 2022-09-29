using Business.BusinessObjects;
using Business.ServiceLayers;
using Core.Interfaces;
using Core.Misc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTO;

namespace WebApi.Controllers
{
    //[ApiController]
    //[EnableCors("CorsPolicy")]
    //public class CaseTypeController : ControllerBase
   // {

        //slBasic _sl;
        //public CaseTypeController(IServiceLayer<slBasic> sl) 
        //{
        //    _sl = sl as slBasic ;

        //}

        //[Route("api/queries/AllCaseTypes")]
        //public  List<dtoClient> GetAllCaseTypes()
        //{
        //    var list = _sl.AllCaseTypes();
        //    return list.Select(c=>new dtoClient {                 
        //        Id=c.Id,
        //        Name=c.Name,
        //        Code=c.Code,
        //        Notes=c.Notes
        //        }).ToList();
        //}

        
        //[Route("api/queries/GetCaseType")]
        //public doCaseType GetCaseType(int id)
        //{
        //    doCaseType ct = _sl.GetCaseType(id);
        //    return ct;
        //}

        //[HttpPost]
        //[Route("api/queries/CreateCaseType")]
        //public object CreateCaseType(dtoClient data)
        //{
        //    doCaseType obj = _sl.CreateNewCaseType(data.Code, data.Name, data.Notes);
        //    try
        //    {
        //        _sl.SaveCaseTypes();
        //        return obj;
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("api/queries/UpdateCaseType")]
        //public object UpdateCaseType(dtoClient _new)
        //{
            
        //    try
        //    {
        //        var obj = _sl.GetCaseType(_new.Id);
        //        if(obj==null)
        //            return BadRequest(new { message = "هذا العنصر غير موجود" });

        //        obj.Name = _new.Name;
        //        obj.Code = _new.Code;
        //        obj.Notes = _new.Notes;

        //        _sl.SaveCaseTypes();

        //        return obj;// Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("api/queries/DeleteCaseType")]
        //public object DeleteCaseType(dynamic data)
        //{

        //    try
        //    {
        //        var obj = _sl.GetCaseType((int)data.Id);
        //        if (obj == null)
        //            return BadRequest(new { message = "هذا العنصر غير موجود" });

        //        try
        //        {
        //            _sl.DeleteCaseType((int)data.Id);
        //            return Ok();
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(new { message = ex.Message });
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }

        //}

            //}
}
