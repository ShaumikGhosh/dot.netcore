using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TelephoneApp.Data;
using TelephoneApp.Interfaces;
using TelephoneApp.Models;


namespace TelephoneApp.Controllers
{
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFContacts _contacts;




        public HomeController(ILogger<HomeController> logger,
            ApplicationContext applicationContext, 
            UserManager<ApplicationUser> userManager,
            IFContacts contacts)
        {
            _logger = logger;
            _applicationContext = applicationContext;
            _userManager = userManager;
            _contacts = contacts;

        }






        [Authorize(Roles = "User")]
        [HttpGet, Route("/")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                ViewBag.FullName = fullName;
            }
            return View(_contacts.GetAll(User));
        }





        [Authorize(Roles = "User")]
        [HttpGet, Route("user/add")]
        public IActionResult AddContact()
        {
            return View();
        }





        [Authorize(Roles = "User")]
        [HttpPost, Route("user/add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddContact(PhonebookModel phonebookModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _applicationContext.ApplicationUser.Find(userId);

            if (ModelState.IsValid)
            {
                _contacts.Insert(phonebookModel, user);
                _contacts.Save();
                TempData["Contact_Created"] = "Contact successfully created!";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }





        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("contact/edit/{id}")]
        public IActionResult Edit (int? id)
        {
            return View(_contacts.GetById(id));
        }






        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("contact/edit/{id}")]
        public IActionResult Edit(PhonebookModel phoneBook, int? id)
        {
            if (ModelState.IsValid)
            {
                _contacts.Update(phoneBook, id);
                _contacts.Save();
                TempData["Contact_Created"] = "Contact successfully updated!";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }




        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("contact/delete/{id}")]
        public IActionResult Delete(int? id)
        {
            _contacts.Delete(id);
            _contacts.Save();
            ViewData["Contact_Created"] = "Record successfully removed!";
            return RedirectToAction("Index", "Home");
        }





        [Authorize(Roles = "User, Admin")]
        [HttpGet, Route("user/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
