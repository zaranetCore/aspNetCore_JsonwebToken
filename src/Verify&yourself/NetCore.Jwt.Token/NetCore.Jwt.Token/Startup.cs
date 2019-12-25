using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCore.Jwt.Token.Models;
using Newtonsoft.Json;

namespace NetCore.Jwt.Token
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //添加Cors跨域
            services.AddCors(options => options.AddPolicy("AllowCorsPolicys", p => p.AllowAnyOrigin()
               .AllowAnyHeader()));
            //添加jwt验证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            /* 
                           * Claims (Payload)
                              Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                              iss: The issuer of the token，token 是给谁的
                              sub: The subject of the token，token 主题
                              exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                              iat: Issued At。 token 创建时间， Unix 时间戳格式
                              jti: JWT ID。针对当前 token 的唯一标识
                              除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                           * */

                            ValidateIssuer = true,//是否验证Issuer
                            ValidateAudience = true,//是否验证Audience
                            ValidateLifetime = true,//是否验证失效时间
                            ClockSkew = TimeSpan.Zero, //默认允许的服务器时间偏移量，设置为0。ClockSkew = TimeSpan.Zero
                            ValidateIssuerSigningKey = true,//是否验证SecurityKey
                            ValidAudience = Configuration["JwtSettings:Issuer"],//Audience
                            ValidIssuer = Configuration["JwtSettings:Audience"],//Issuer，这两项和前面签发jwt的设置一致
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"]))//SecretKey
                        };
                        options.Events = new JwtBearerEvents
                        {
                            //OnAuthenticationFailed = context =>
                            //{
                            //    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            //    logger.LogError("Authentication failed.", context.Exception);
                            //    ResponseResult responseResult = new ResponseResult<TokenResponseResult>()
                            //    {
                            //        Code = HttpStatusCode.Unauthorized,
                            //        Message = "对不起！接口身份授权失败，您无权访问！"
                            //    };
                            //    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            //    context.Response.WriteAsync(JsonConvert.SerializeObject(responseResult, Formatting.Indented));
                            //    return Task.CompletedTask;
                            //}
                        };
                    });


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //开启跨域，UseCors 必须在UseMvc 之前调用。
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); //UseCors 必须在UseAuthentication之前调用
            app.UseAuthorization();
            //开启jwt验证
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
