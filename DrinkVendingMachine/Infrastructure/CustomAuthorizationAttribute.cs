using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DrinkVendingMachine.Infrastructure
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        string key;

        public CustomAuthorizationAttribute(string key)
        {
            this.key = key;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Query.ContainsKey("key") || context.HttpContext.Request.Query["key"].ToString() != key)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
