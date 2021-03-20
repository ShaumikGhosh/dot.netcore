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
using TelephoneApp.Interfaces;
using TelephoneApp.Models;
using TelephoneApp.Services;


namespace TelephoneApp.Controllers
{
    public class SignupController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;




        public SignupController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }



        [HttpGet, AllowAnonymous, Route("user/signup")]
        public IActionResult Register()
        {
            var model = new SignupModel();
            return View(model);
        }






        [HttpPost, AllowAnonymous, Route("user/signup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignupModel request)
        {
            if (ModelState.IsValid)
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

                var userCheck = await _userManager.FindByEmailAsync(request.Email);

                if (userCheck == null)
                {
                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");

                        TempData["Message"] = "Successfully Registration Done";
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        if (result.Errors.Any())
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
