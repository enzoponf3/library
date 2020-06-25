using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PonfeLibrary.Models;

namespace PonfeLibrary.Controllers
{
    public class PublisherController : Controller
    {
        LibraryContext db;
        public PublisherController(LibraryContext lib)
        {
            db = lib;
        }
        public IActionResult Index()
        {
            List<Publisher> publishers = db.Publisher.OrderBy(p => p.Name).ToList();
            ViewBag.Publishers = publishers;
            return View();
        }
    }
}
