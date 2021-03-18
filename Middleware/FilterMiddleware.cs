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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public FilterMiddleware(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public FilterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.QueryString.ToString().Contains("ReturnUrl"))
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

            if (httpContext.Request.Path.ToString().Contains("/Identity/") || httpContext.Request.Path.ToString().Contains("/identity/"))
            {
                httpContext.Response.Redirect("/home/error");
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
