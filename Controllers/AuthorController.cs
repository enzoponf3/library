using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PonfeLibrary.Models;

namespace PonfeLibrary.Controllers
{
    public class AuthorController : Controller
    {
        LibraryContext db;

        public AuthorController(LibraryContext lib)
        {
            db = lib;
        }
        public IActionResult Index()
        {
            List<Author> authors = db.Author.OrderBy(a => a.Name).ToList();
            ViewBag.Authors = authors;
            return View();
        }
    }
}
