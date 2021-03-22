using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelephoneApp.Models;


namespace TelephoneApp.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class FilterMiddleware
    {
        private readonly RequestDelegate _next;

        public FilterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Query.ContainsKey("ReturnUrl") == true)
            {
                httpContext.Response.Redirect("/user/login");
            }
            else if (httpContext.Request.Path == "/user/login")
            {
                if (httpContext.Request.Cookies["logedin"] != null) {
                    
                    if(httpContext.User.IsInRole("Admin"))
                    {
                        httpContext.Response.Redirect("/admin/dashboard");
                    }
                    else if (httpContext.User.IsInRole("User"))
                    {
                        httpContext.Response.Redirect("/");
                    }
                } 
               
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class FilterMiddlewareExtensions
    {
        public static IApplicationBuilder UseFilterMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FilterMiddleware>();
        }
    }
}
