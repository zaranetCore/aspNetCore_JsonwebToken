using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_Cookie_Sample.Controllers
{
    public class LoginOrSignOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (loginModel.Username == "haozi zhang" &&
                loginModel.Password == "123456")
            {
                var claims = new List<Claim>
                 {
                 new Claim(ClaimTypes.Name, loginModel.Username)
                 };
                ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "login"));
                await HttpContext.SignInAsync(principal);
                //Just redirect to our index after logging in. 
                return Redirect("/Home/Index");
            }
            return View("Index");
        }
        /// <summary>
        /// this action for web lagout 
        /// </summary>
        [HttpGet]
        public IActionResult Logout()
        {
            Task.Run(async () =>
            {
                //注销登录的用户，相当于ASP.NET中的FormsAuthentication.SignOut  
                await HttpContext.SignOutAsync();
            }).Wait();
            return View();
        }
    }
}