using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1_IsTp__2;

namespace Lab1_IsTp__2.Controllers
{
    public class EuroCupsController : Controller
    {
        private readonly FootballContext _context;

        public EuroCupsController(FootballContext context)
        {
            _context = context;
        }

        // GET: EuroCups
        public async Task<IActionResult> Index()
        {
            return View(await _context.EuroCups.ToListAsync());
        }

        // GET: EuroCups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var euroCup = await _context.EuroCups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (euroCup == null)
            {
                return NotFound();
            }

            // return View(euroCup);
            return RedirectToAction("Index", "Clubs", new { id = euroCup.Id, name = euroCup.Name, typeTournament = "EuroCups" });
        }

        // GET: EuroCups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EuroCups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ClubsNumber")] EuroCup euroCup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(euroCup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(euroCup);
        }

        // GET: EuroCups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var euroCup = await _context.EuroCups.FindAsync(id);
            if (euroCup == null)
            {
                return NotFound();
            }
            return View(euroCup);
        }

        // POST: EuroCups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ClubsNumber")] EuroCup euroCup)
        {
            if (id != euroCup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(euroCup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EuroCupExists(euroCup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(euroCup);
        }

        // GET: EuroCups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var euroCup = await _context.EuroCups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (euroCup == null)
            {
                return NotFound();
            }

            return View(euroCup);
        }

        // POST: EuroCups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var euroCup = await _context.EuroCups.FindAsync(id);
            _context.EuroCups.Remove(euroCup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EuroCupExists(int id)
        {
            return _context.EuroCups.Any(e => e.Id == id);
        }
    }
}
