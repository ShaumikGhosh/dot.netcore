using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class LoginController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;




        public LoginController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
        }





        [HttpGet]
        [AllowAnonymous]
        [Route("user/login")]
        public IActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }






        [HttpPost]
        [AllowAnonymous]
        [Route("user/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    HttpContext.Response.Cookies.Append("logedin", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Dashboard", "Dashboard");
                    }

                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }



    }
}
