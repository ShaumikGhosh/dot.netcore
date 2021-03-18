using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelephoneApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/{status}")]
        public IActionResult Error(int status)
        {
            switch (status)
            {
                case 404:
                    ViewBag.ErrorMessage = "Page not found";
                    break;
            }
            return View();
        }
    }
}
