using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        public IActionResult Show()
        {
            List<Book> books = db.Book.OrderBy(b => b.Title).ToList();
            ViewBag.Books = books;
            return View("Views/Book/Book.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = db.Book.
                Include(b => b.BookAuth).
                    ThenInclude(a => a.Auth).
                Include(b => b.BookGen).
                    ThenInclude(g => g.Gen).
                Include(b => b.Pub).
                Where(b => b.Id == id).
                FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }
            return View("Edit", book);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var bk = db.Book.
                Include(b=>b.BookAuth).
                    ThenInclude(a=>a.Auth).
                Include(b=>b.BookGen).
                    ThenInclude(g=>g.Gen).
                Include(b => b.Pub).
                Where(b => b.Id ==id).
                FirstOrDefault();
            if(bk == null)
            {
                return NotFound();
            }
            return new ObjectResult(bk);
        }
    }
   
}
