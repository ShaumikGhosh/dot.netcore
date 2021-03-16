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
using TelephoneApp.Models;


namespace TelephoneApp.Controllers
{
    // Shaumik Ghosh (Coding with C# ASP.NET Core MVC5)
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<IdentityUser> _userManager;




        public HomeController(ILogger<HomeController> logger,
            ApplicationContext applicationContext, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _applicationContext = applicationContext;
            _userManager = userManager;
        }




        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _applicationContext.ApplicationUser.Find(userId);

            if (user != null)
            {
                string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                ViewBag.FullName = fullName;
            }
            var records = await _applicationContext.PhonebookRecords.Where(x=>x.user == user).ToListAsync();
            return View(records);
        }




        [HttpGet, Route("user/add")]
        public IActionResult AddContact()
        {
            return View();
        }




        [HttpPost, Route("user/add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddContact([Bind("FullName", "PhoneNumber", "Address", "Approved", "user")]  PhonebookModel phonebookModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _applicationContext.ApplicationUser.Find(userId);

            if (ModelState.IsValid)
            {
                PhonebookModel pm = new PhonebookModel();
                pm.FullName = phonebookModel.FullName;
                pm.PhoneNumber = phonebookModel.PhoneNumber;
                pm.Address = phonebookModel.Address;
                pm.user = user;
                _applicationContext.PhonebookRecords.Add(pm);
                _applicationContext.SaveChanges();
                TempData["Contact_Created"] = "New contact successfully added";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }






        [HttpGet]
        [Route("contact/edit/{id}")]
        public IActionResult Edit (int id)
        {
            var record = _applicationContext.PhonebookRecords.Find(id);
            return View(record);
        }




        [HttpPost]
        [Route("contact/edit/{id}")]
        public IActionResult Edit(PhonebookModel phoneBook, int id)
        {
            if (ModelState.IsValid)
            {
                var record = _applicationContext.PhonebookRecords.Find(id);
                record.FullName = phoneBook.FullName;
                record.PhoneNumber = phoneBook.PhoneNumber;
                record.Address = phoneBook.Address;
                _applicationContext.PhonebookRecords.Update(record);
                _applicationContext.SaveChanges();
                TempData["Contact_Created"] = "Contact successfully updated!";

                return RedirectToAction("Index", "Home");
            }
            return View();
        }





        [HttpGet]
        [Route("contact/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var contact = _applicationContext.PhonebookRecords.Find(id);
            _applicationContext.Remove(contact);
            _applicationContext.SaveChanges();
            ViewData["Contact_Created"] = "Record successfully removed!";
            return RedirectToAction("Index", "Home");
        }




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
