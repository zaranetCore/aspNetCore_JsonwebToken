using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace DotNetCore_Jwt_Client.Auth_Center
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var result = await httpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("Authorize error");
            }
            else
            {
                httpContext.User = result.Principal;
                await _next.Invoke(httpContext);
            }
        }
    }
}
