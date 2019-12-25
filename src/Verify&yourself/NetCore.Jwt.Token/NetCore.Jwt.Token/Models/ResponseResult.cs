using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NetCore.Jwt.Token.Models
{
    public class ResponseResult
    {
        [JsonProperty("code", Order = 1)]
        public HttpStatusCode Code { get; set; }

        [JsonProperty("message", Order = 2)]
        public string Message { get; set; }
    }
    public class ResponseResult<T> : ResponseResult where T : class
    {
        [JsonProperty("data", Order = 3)]
        public T Data { get; set; }
    }
}
