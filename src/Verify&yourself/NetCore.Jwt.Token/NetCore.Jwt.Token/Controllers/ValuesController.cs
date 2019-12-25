using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.Jwt.Token.Controllers
{
    [Authorize] //可以作用在控制器或方法上。
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// 我必须进行授权才能访问
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "这个接口需要授权才能访问",
                                  "获取用户Name:"+HttpContext.User.Identity.Name,
                                  "获取用户uid:"+HttpContext.User.FindFirst("uid")?.Value,
                                  "获取用户uName:"+HttpContext.User.FindFirst("uName")?.Value
                                };
        }
        /// <summary>
        /// 我不需要授权也能访问  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}")]
        [AllowAnonymous]//在不需要授权访问的action上加此特性
        public ActionResult<string> Get(int id)
        {
            return $"[id={id}]:这个接口不需要授权访问";
        }


       

       
    }
}
