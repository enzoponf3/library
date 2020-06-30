using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        public IActionResult Show()
        {
            List<Genre> genres = db.Genre.OrderBy(g => g.Name).ToList();
            ViewBag.Genres = genres;
            return View("Views/Genre/Genre.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gen = await db.Genre.FindAsync(id);
            if (gen == null)
            {
                return NotFound();
            }
            return View(gen);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Nationality")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(genre);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Show));
            }
            return View(genre);
        }
    }
}
