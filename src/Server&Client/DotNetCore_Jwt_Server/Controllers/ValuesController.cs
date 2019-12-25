using Microsoft.AspNetCore.Mvc;
using DotNetCore_Jwt_Server.Services;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace DotNetCore_Jwt_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public ValuesController(IUserService userService,
            ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpGet]
        public async Task<string> Get()
        {
            await Task.CompletedTask;
            return "Welcome the Json Web Token Solucation!";
        }
        [HttpGet("getToken")]
        public async Task<string> GetTokenAsync(string name, string password)
        {
            var user = await _userService.LoginAsync(name, password);
            if (user == null)
                return "Login Failed";

            var token = _tokenService.GetToken(user);
            var response = new
            {
                Status = true,
                Token = token,
                Type = "Bearer"
            };
            return JsonConvert.SerializeObject(response);
        }
    }
}
