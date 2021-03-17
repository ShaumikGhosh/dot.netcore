using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelephoneApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet, Route("roles")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }


        [HttpGet, Route("role/create")]
        public IActionResult Create ()
        {
            return View();
        }


        [HttpPost, Route("role/create")]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index", "Role");
        }
    }
}
