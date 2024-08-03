using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Common.Common
{
    public class UserContext
    {
        private static IHttpContextAccessor _accessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }
        public static string Current => GetUserId();

        private static string GetUserId()
        {
            if (_accessor.HttpContext.User.Claims == null && !_accessor.HttpContext.User.Claims.Any())
                return "System";
            return _accessor.HttpContext.User?.Claims?.FirstOrDefault(f => f.Type == "UserId")?.Value ?? null;
        }
    }
}
