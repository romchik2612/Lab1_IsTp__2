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
    public class CupsController : Controller
    {
        private readonly FootballContext _context;

        public CupsController(FootballContext context)
        {
            _context = context;
        }

        // GET: Cups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cups.ToListAsync());
        }

        // GET: Cups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cup = await _context.Cups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cup == null)
            {
                return NotFound();
            }

            //return View(cup);
            return RedirectToAction("Index", "Clubs", new { id = cup.Id, name = cup.Name, typeTournament = "Cups" });
        }
    

        // GET: Cups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ClubsNumber")] Cup cup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cup);
        }

        // GET: Cups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cup = await _context.Cups.FindAsync(id);
            if (cup == null)
            {
                return NotFound();
            }
            return View(cup);
        }

        // POST: Cups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ClubsNumber")] Cup cup)
        {
            if (id != cup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CupExists(cup.Id))
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
            return View(cup);
        }

        // GET: Cups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cup = await _context.Cups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cup == null)
            {
                return NotFound();
            }

            return View(cup);
        }

        // POST: Cups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cup = await _context.Cups.FindAsync(id);
            _context.Cups.Remove(cup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CupExists(int id)
        {
            return _context.Cups.Any(e => e.Id == id);
        }
    }
}
