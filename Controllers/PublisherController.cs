﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        public IActionResult Show()
        {
            List<Publisher> publishers = db.Publisher.OrderBy(p => p.Name).ToList();
            ViewBag.Publishers = publishers;
            return View("Views/Publisher/Publisher.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pub = await db.Publisher.FindAsync(id);
            if (pub == null)
            {
                return NotFound();
            }
            return View(pub);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Nationality")] Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(publisher);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Show));
            }
            return View(publisher);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View("Add");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name")] Publisher pub)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(pub);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Show));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Please try again.");
            }
            return View(nameof(Show));
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            Publisher pub = await db.Publisher.FindAsync(id);
            try
            {
                if (pub == null)
                {
                    return RedirectToAction(nameof(Show));

                }
                db.Publisher.Remove(pub);
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
