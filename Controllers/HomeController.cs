using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using login.Models;

namespace login.Controllers
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

            var UserName = Request.Cookies["UserName"];
            ViewBag.User = UserName;
            _logger.LogError(UserName);
            return View();
        }

        public IActionResult Privacy()
        {
            var UserName = Request.Cookies["UserName"];
            ViewBag.User = UserName;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var UserName = Request.Cookies["UserName"];
            ViewBag.User = UserName;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
