using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt_Policy_Demo.AuthMiddler
{
    /// <summary>
    /// Rights-carrying entity
    /// </summary>
    public class PolicyRequirement : IAuthorizationRequirement
    {/// <summary>
     /// User rights collection
     /// </summary>
        public List<UserPermission> UserPermissions { get; private set; }
        /// <summary>
        /// No permission action
        /// </summary>
        public string DeniedAction { get; set; }
        /// <summary>
        /// structure
        /// </summary>
        public PolicyRequirement()
        {
            //Jump to this route without permission
            DeniedAction = new PathString("/api/nopermission");
            //Route configuration that users have access to, of course you can read it from the database, you can also put it in Redis for persistence
            UserPermissions = new List<UserPermission> {
                              new UserPermission {  Url="/api/value3", UserName="admin"},
                          };
        }
    }
    public class UserPermission
    {
        public string UserName { get; set; }
        public string Url { get; set; }
    }
}
