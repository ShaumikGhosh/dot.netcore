using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TelephoneApp.Middlwares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenthicationMiddlware
    {
        private readonly RequestDelegate _next;

        public AuthenthicationMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string[] urls = {
                "/", 
                "/user/privacy",
                "/user/add"
            };

            foreach (var url in urls)
            {
                if (httpContext.Request.Path == url)
                {
                    if (!httpContext.User.Identity.IsAuthenticated)
                    {
                        httpContext.Response.Redirect("/user/login");
                    }
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenthicationMiddlwareExtensions
    {
        public static IApplicationBuilder UseAuthenthicationMiddlware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenthicationMiddlware>();
        }
    }
}
