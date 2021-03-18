using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TelephoneApp.Data;
using TelephoneApp.Models;

namespace TelephoneApp.Controllers
{
    public class SignupController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _applicationContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signinManager;



        public SignupController(
            UserManager<ApplicationUser> userManager,
            ApplicationContext context,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager
        )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _applicationContext = context;
            _signinManager = signInManager;
        }



        [HttpGet, AllowAnonymous, Route("user/signup")]
        public IActionResult Register()
        {
            SignupModel model = new SignupModel();
            return View(model);
        }






        [HttpPost, AllowAnonymous, Route("user/signup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignupModel request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await _userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new ApplicationUser
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };

                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");

                        TempData["Message"] = "Successfully Registration Done";
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);
        }





    }
}
