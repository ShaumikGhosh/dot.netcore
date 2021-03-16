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


        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationContext _applicationContext;

        public SignupController(UserManager<IdentityUser> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _applicationContext = context;
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

                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var userId2 = _applicationContext.ApplicationUser.Find(userId);

                    await _userManager.AddToRoleAsync(userId2, "User");

                    if (result.Succeeded)
                    {
                        TempData["Message"] = "Successfully Registration Done";
                        return RedirectToAction("Register", "Signup");
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
