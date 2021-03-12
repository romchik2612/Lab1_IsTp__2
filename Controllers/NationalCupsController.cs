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
    public class NationalCupsController : Controller
    {
        private readonly FootballContext _context;

        public NationalCupsController(FootballContext context)
        {
            _context = context;
        }

        // GET: NationalCups
        public async Task<IActionResult> Index()
        {
            return View(await _context.NationalCups.ToListAsync());
        }

        // GET: NationalCups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationalCup = await _context.NationalCups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nationalCup == null)
            {
                return NotFound();
            }

            //return View(nationalCup);
            return RedirectToAction("Index", "Nationals", new { id = nationalCup.Id, name = nationalCup.Name });
        }

        // GET: NationalCups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NationalCups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TeamsNumber")] NationalCup nationalCup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nationalCup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nationalCup);
        }

        // GET: NationalCups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationalCup = await _context.NationalCups.FindAsync(id);
            if (nationalCup == null)
            {
                return NotFound();
            }
            return View(nationalCup);
        }

        // POST: NationalCups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TeamsNumber")] NationalCup nationalCup)
        {
            if (id != nationalCup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nationalCup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NationalCupExists(nationalCup.Id))
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
            return View(nationalCup);
        }

        // GET: NationalCups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationalCup = await _context.NationalCups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nationalCup == null)
            {
                return NotFound();
            }

            return View(nationalCup);
        }

        // POST: NationalCups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nationalCup = await _context.NationalCups.FindAsync(id);
            _context.NationalCups.Remove(nationalCup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NationalCupExists(int id)
        {
            return _context.NationalCups.Any(e => e.Id == id);
        }
    }
}
