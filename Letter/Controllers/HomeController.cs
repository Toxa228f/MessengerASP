using Letter.Dal.Interfaces;
using Letter.Domain.Entities;
using Letter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Letter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository rep;

        public HomeController(ILogger<HomeController> logger, IUserRepository rep)
        {
            _logger = logger;
            this.rep = rep;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            long id = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            if (id == 0)
            {
                return RedirectToAction("Log", "Login");
            }
            User user = await rep.GetById(id);
            return View(user);
        }

        [Authorize]
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
