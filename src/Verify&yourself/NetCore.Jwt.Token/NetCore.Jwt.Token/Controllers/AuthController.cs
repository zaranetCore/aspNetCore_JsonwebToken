using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCore.Jwt.Token.Models;

namespace NetCore.Jwt.Token.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet]
        public Models.ResponseResult<TokenResponseResult> GetToken(string userid,string password)
        {
            if (string.IsNullOrWhiteSpace(userid))
            {
                return new ResponseResult<TokenResponseResult> { Code=HttpStatusCode.BadRequest,Message="用户名不能为空"};
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return new ResponseResult<TokenResponseResult> { Code=HttpStatusCode.BadRequest,Message="密码不能为空"};
            }
            try
            {
                //模拟操作数据库
                if (userid.Equals("feifei") && password.Equals("123456"))
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("n")),
                        new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                        new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddHours(8)).ToUnixTimeSeconds()}"),
                        new Claim(ClaimTypes.Name, "飞飞"),
                        new Claim("uid","123456789"),
                        new Claim("uName","ZaraNet'Jwt授权验证"),
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(issuer: Configuration["JwtSettings:Issuer"],
                                                     audience: Configuration["JwtSettings:Audience"],
                                                     claims: claims,
                                                     expires: DateTime.Now.AddHours(8),
                                                     signingCredentials: creds);

                    string appToken = new JwtSecurityTokenHandler().WriteToken(token);

                    return new ResponseResult<TokenResponseResult>()
                    {
                        Code = HttpStatusCode.OK,
                        Message = "success",
                        Data = new TokenResponseResult
                        {
                            UserId = userid,
                            Token = appToken,
                        }
                    };
                }
                else
                {
                    return new ResponseResult<TokenResponseResult> { Code=HttpStatusCode.BadRequest,Message="请使用userid=feifei,password=123456操作"};
                }
            }
            catch (Exception)
            {

                return new ResponseResult<TokenResponseResult> { Code=HttpStatusCode.BadRequest,Message="获取令牌失败"};
            }
        }

    }
}