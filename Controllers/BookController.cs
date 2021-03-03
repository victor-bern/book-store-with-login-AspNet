using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using login.Models;
using Microsoft.AspNetCore.Http;
using login.Data;
using Microsoft.EntityFrameworkCore;

namespace login.Controllers
{

    public class BookController : Controller
    {

        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {

            _logger = logger;


        }

        [HttpGet]
        public IActionResult AddBook()
        {
            var UserName = Request.Cookies["UserName"];
            ViewBag.User = UserName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromServices] DataContext context, Book model)
        {

            var UserId = Request.Cookies["UserId"];

            if (!ModelState.IsValid)
                return View(model);

            model.UserId = int.Parse(UserId);
            context.Books.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> ReadBook([FromServices] DataContext context, string Id, string UserId, string Name)
        {

            var book = new Book
            {
                Name = Name,
                UserId = int.Parse(UserId),
                Id = int.Parse(Id),
                IsRead = true
            };
            context.Entry<Book>(book).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
