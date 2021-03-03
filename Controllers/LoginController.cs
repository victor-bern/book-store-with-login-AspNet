using System.Linq;
using System.Threading.Tasks;
using login.Data;
using login.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Logging;

namespace login.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromServices] DataContext context)
        {
            if (Request.Cookies["UserName"] == null)
                return RedirectToAction("Login");
            var UserName = Request.Cookies["UserName"];
            var UserId = Request.Cookies["UserId"];

            var books = await context.Books
            .AsNoTracking()
            .Where(x => x.UserId == int.Parse(UserId))
            .ToListAsync();

            ViewData["User"] = UserName;

            return View(books);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var UserName = Request.Cookies["UserName"];
            if (UserName != null)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind] User model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await context.Users
            .AsNoTracking()
            .Where(x => x.Email == model.Email && x.Password == model.Password)
            .FirstOrDefaultAsync();

            if (user == null)
                return RedirectToAction("Login");

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("UserName", user.Name, cookieOptions);
            Response.Cookies.Append("UserId", user.Id.ToString(), cookieOptions);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var UserName = Request.Cookies["UserName"];
            if (UserName != null)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind] User model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return View(model);

            context.Users.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append("UserName", "", cookieOptions);
            Response.Cookies.Append("UserId", "", cookieOptions);
            return RedirectToAction("Login");
        }
    }
}