using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_Jwt_Client.Auth_Center;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_Jwt_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public ValuesController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpGet]
        [Authorize]
        public async Task<string> Get()
        {
            await Task.CompletedTask;
            return $"{_identityService.GetUserId()}:{_identityService.GetUserName()}";
        }
    }
}