using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MStest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MStest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CurrentUser.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            CurrentUser.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
