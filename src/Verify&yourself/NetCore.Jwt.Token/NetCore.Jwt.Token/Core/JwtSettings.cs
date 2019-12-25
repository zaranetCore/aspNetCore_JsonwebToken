using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Jwt.Token.Core
{
    public class JwtSettings
    {
        /// <summary>
        /// token是谁颁发的
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// token可以给哪些客户端使用
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        ///加密的key 这里为了演示，写死一个密钥。实际生产环境可以从配置文件读取,这个是用网上工具随便生成的一个密钥。
        /// the secret that needs to be at least 16 characeters long for HmacSha256"
        /// </summary>
        public string SecretKey { get; set; }
    }
}
