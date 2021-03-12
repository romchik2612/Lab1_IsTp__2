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
    public class NationalsController : Controller
    {
        private readonly FootballContext _context;

        public NationalsController(FootballContext context)
        {
            _context = context;
        }

        // GET: Nationals
        public async Task<IActionResult> Index(int? id,string? name)
        {
           // var footballContext = _context.Nationals.Include(n => n.NationalCup);
            //return View(await footballContext.ToListAsync());
            if (id == null) return RedirectToAction("Index", "NationalCups");
            ViewBag.NationalCupId = id;
            ViewBag.NationalCupName = name;
            var nationalsToCup = _context.Nationals.Where(n => n.NationalCupId == id).Include(n => n.NationalCup);
            return View(await nationalsToCup.ToListAsync());
        }

        // GET: Nationals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var national = await _context.Nationals
                .Include(n => n.NationalCup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (national == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Footbollers", new { id = national.Id, name = national.Name, teamType = "nation" });
        }

        // GET: Nationals/Create
        public IActionResult Create(int nationalCupId)
        {
            ViewBag.NationalCupId = nationalCupId;
            ViewBag.NationalCupName = _context.NationalCups.Where(m => m.Id == nationalCupId).FirstOrDefault().Name;
            return View();

        }

        // POST: Nationals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int nationalCupId, [Bind("Id,Name,TrophiesNumber,NationalCupId")] National national)
        {
            national.NationalCupId = nationalCupId;
            if (ModelState.IsValid)
            {
                _context.Add(national);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Nationals", new { id = nationalCupId, name = _context.NationalCups.Where(m => m.Id == nationalCupId).FirstOrDefault().Name });
            }
            ViewData["NationalCupId"] = new SelectList(_context.NationalCups, "Id", "Name", national.NationalCupId);
            // return View(national);
            return RedirectToAction("Index", "Nationals", new { id = nationalCupId, name = _context.NationalCups.Where(m => m.Id == nationalCupId).FirstOrDefault().Name });

        }

        // GET: Nationals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var national = await _context.Nationals.FindAsync(id);
            if (national == null)
            {
                return NotFound();
            }
            ViewData["NationalCupId"] = new SelectList(_context.NationalCups, "Id", "Name", national.NationalCupId);
            return View(national);
        }

        // POST: Nationals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TrophiesNumber,NationalCupId")] National national)
        {
            if (id != national.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(national);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NationalExists(national.Id))
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
            ViewData["NationalCupId"] = new SelectList(_context.NationalCups, "Id", "Name", national.NationalCupId);
            return View(national);
        }

        // GET: Nationals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var national = await _context.Nationals
                .Include(n => n.NationalCup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (national == null)
            {
                return NotFound();
            }

            return View(national);
        }

        // POST: Nationals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var national = await _context.Nationals.FindAsync(id);
            _context.Nationals.Remove(national);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NationalExists(int id)
        {
            return _context.Nationals.Any(e => e.Id == id);
        }
    }
}
