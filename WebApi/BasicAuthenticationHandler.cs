using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Core.Interfaces;
using DAL.Tables;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace WebApi
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
       // private readonly AuthUOW _uow;
        private string failReason;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions>options,
            ILoggerFactory logger,
            UrlEncoder encoder ,
            ISystemClock clock//,  IUnitOfWork<AuthUOW>  uow
            ):base(options,logger,encoder,clock)
        {
          //  _uow = uow as AuthUOW;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                failReason = "authorization header not sent";
                return AuthenticateResult.Fail(failReason);// unauthorized 401 error
            }

            try
            {
              

                    return AuthenticateResult.Success(null);

                
            }
            catch(Exception ex)
            {
                failReason = ex.Message;
                return AuthenticateResult.Fail(failReason);// unauthorized 401 error
            }

            
        }
    }
}
