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
    public class FootbollersController : Controller
    {
        private readonly FootballContext _context;

        public FootbollersController(FootballContext context)
        {
            _context = context;
        }

        // GET: Footbollers
        public async Task<IActionResult> Index(int? id,string? name, string? teamType)
        {
            if (teamType == "club")
            {
                if (id == null) return RedirectToAction("Index", "Clubs");
                ViewBag.TeamId = id;
                ViewBag.TeamName = name;
                var footbollersByTeam = _context.Footbollers.Where(f => f.ClubId == id).Include(f => f.Club);
                return View(await footbollersByTeam.ToListAsync());
            }
            else if (teamType == "nation")
            {
                if (id == null) return RedirectToAction("Index", "Nationals");
                ViewBag.TeamId = id;
                ViewBag.TeamName = name;
                var footbollersByTeam = _context.Footbollers.Where(f => f.NationalId == id).Include(f => f.National);
                return View(await footbollersByTeam.ToListAsync());
            }
           

            else return RedirectToAction("Index", "Clubs");
        }

        // GET: Footbollers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footboller = await _context.Footbollers
                .Include(f => f.Club)
                .Include(f => f.National)
                .FirstOrDefaultAsync(m => m.Id == id);
           
            if (footboller == null)
            {
                return NotFound();
            }

            return View(footboller);
        }

        // GET: Footbollers/Create
        public IActionResult Create()
        {
            ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "Name");
            ViewData["NationalId"] = new SelectList(_context.Nationals, "Id", "Name");
            return View();
        }

        // POST: Footbollers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Sourname,Position,NationalId,ClubId,GoalsNumber,AssistsNumber,DateOfBirth,MatchesNumber")] Footboller footboller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(footboller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "Name", footboller.ClubId);
            ViewData["NationalId"] = new SelectList(_context.Nationals, "Id", "Name", footboller.NationalId);
            return View(footboller);
        }

        // GET: Footbollers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footboller = await _context.Footbollers.FindAsync(id);
            if (footboller == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "DateOfBirth", footboller.ClubId);
            ViewData["NationalId"] = new SelectList(_context.Nationals, "Id", "Name", footboller.NationalId);
            return View(footboller);
        }

        // POST: Footbollers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Sourname,Position,NationalId,ClubId,GoalsNumber,AssistsNumber,DateOfBirth,MatchesNumber")] Footboller footboller)
        {
            if (id != footboller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(footboller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FootbollerExists(footboller.Id))
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
            ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "DateOfBirth", footboller.ClubId);
            ViewData["NationalId"] = new SelectList(_context.Nationals, "Id", "Name", footboller.NationalId);
            return View(footboller);
        }

        // GET: Footbollers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footboller = await _context.Footbollers
                .Include(f => f.Club)
                .Include(f => f.National)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (footboller == null)
            {
                return NotFound();
            }

            return View(footboller);
        }

        // POST: Footbollers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var footboller = await _context.Footbollers.FindAsync(id);
            _context.Footbollers.Remove(footboller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FootbollerExists(int id)
        {
            return _context.Footbollers.Any(e => e.Id == id);
        }
    }
}
