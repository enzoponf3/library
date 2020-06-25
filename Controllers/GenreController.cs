using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PonfeLibrary.Models;

namespace PonfeLibrary.Controllers
{
    public class GenreController : Controller
    {
        LibraryContext db;
        public GenreController(LibraryContext lib)
        {
            db = lib;
        }
        public IActionResult Index()
        {
            List<Genre> genres = db.Genre.OrderBy(g => g.Name).ToList();
            ViewBag.Genres = genres;
            return View();
        }
    }
}
