using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelephoneApp.Models;

namespace TelephoneApp.Controllers.Admin
{
    public class DashboardController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;




        public DashboardController(
            ILogger<DashboardController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }




        [Route("admin/dashboard")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                ViewBag.FullName = fullName;
            }
            return View();
        }
    }
}
