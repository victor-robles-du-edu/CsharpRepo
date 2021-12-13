using WebAppStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppStore.Controllers
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
            return View();
        }

        public IActionResult Laptops()
        {
            return View();
        }

        public IActionResult Tablets()
        {
            return View();
        }

        public IActionResult Accessories()
        {
            return View();
        }

        public IActionResult Locations()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult MailingLists()
        {
            return View();
        }

        public IActionResult Quotes()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Home()
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
