using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PonfeLibrary.Models;

namespace PonfeLibrary.Controllers
{
    public class BookController : Controller
    {
        LibraryContext db;
        public BookController (LibraryContext lib)
        {
            db = lib;
        }
        public IActionResult Index()
        {
            List<Book> books = db.Book.OrderBy(b => b.Title).ToList();
            ViewBag.Books = books;
            return View();
        }
    }
}
