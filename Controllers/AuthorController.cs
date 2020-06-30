using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        public IActionResult Show()
        {
            List<Author> authors = db.Author.OrderBy(a => a.Name).ToList();
            ViewBag.Authors = authors;
            return View("Views/Author/Author.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var au = await db.Author.FindAsync(id);
            if (au == null)
            {
                return NotFound();
            }
            return View("Edit",au);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Nationality")] Author author )
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(author);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Show));
            }
            return View(author);
        }
    }
}
