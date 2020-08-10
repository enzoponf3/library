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
        protected Book complementInformation (int? id)
        {
            if (id == null)
            {
                return null;
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
                return null;
            }
            return book;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            Book book = complementInformation(id);
            if (book == null) return NotFound();
            List<Genre> genres = db.Genre.OrderBy(g => g.Name).ToList();
            List<Author> authors = db.Author.OrderBy(a => a.Name).ToList();
            List<Publisher> publishers = db.Publisher.OrderBy(p => p.Name).ToList();
            ViewBag.Authors = authors;
            ViewBag.Genres = genres;
            ViewBag.Publishers = publishers;
            return View("Edit", book);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {

            Book book = complementInformation(id);
            if(book == null)return NotFound();
            return new ObjectResult(book);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View("Add");
        }
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Title, Subtitle, Isbn, Authors, Pub, Genre")] Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(book);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Show));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Please try again.");
            }
            return View(nameof(Show));
        }*/
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            Book book = await db.Book.FindAsync(id);
            try
            {
                if (book == null)
                {
                    return RedirectToAction(nameof(Show));

                }
                db.Book.Remove(book);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Show));

            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Please try again.");
            }
            return RedirectToAction(nameof(Show));
        }
    }
   
}
