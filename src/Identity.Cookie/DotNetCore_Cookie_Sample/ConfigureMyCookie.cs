using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_Cookie_Sample
{
    internal class ConfigureMyCookie : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        // You can inject services here
        public ConfigureMyCookie()
        {
        }
        public void Configure(string name, CookieAuthenticationOptions options)
        {
            // Only configure the schemes you want
            //if (name == Startup.CookieScheme)
            //{
                // options.LoginPath = "/someotherpath";
            //}
        }

        public void Configure(CookieAuthenticationOptions options)
            => Configure(Options.DefaultName, options);
    }
}
