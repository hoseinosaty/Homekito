using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barayand.Common.Services
{
    public static class AuthRequestService
    {
        public static ResponseStructure Auth(HttpContext context)
        {
            Microsoft.Extensions.Primitives.StringValues header = "";
            context.Request.Headers.TryGetValue("Postman-Token", out header);
            return null;
        }
    }
}
