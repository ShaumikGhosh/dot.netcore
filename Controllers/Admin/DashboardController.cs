using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelephoneApp.Controllers.Admin
{
    public class DashboardController : Controller
    {
        [Route("admin/dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
