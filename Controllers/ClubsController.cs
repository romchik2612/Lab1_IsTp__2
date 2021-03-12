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
    public class ClubsController : Controller
    {
        private readonly FootballContext _context;

        public ClubsController(FootballContext context)
        {
            _context = context;
        }

        // GET: Clubs
        public async Task<IActionResult> Index(int? id, string? name, string? typeTournament)
        {
            // var footballContext = _context.Clubs.Include(c => c.Cup).Include(c => c.EuroCup).Include(c => c.League).Include(c => c.National);
            //  return View(await footballContext.ToListAsync());
            if (typeTournament == "Cups")
            {
                if (id == null) return RedirectToAction("Index", "Cups");
                ViewBag.CupId = id;
                ViewBag.CupName = name;
                var clubsToCup = _context.Clubs.Where(c => c.CupId == id).Include(c => c.Cup);
                return View(await clubsToCup.ToListAsync());
            }
            else if (typeTournament == "Leagues") {
                if (id == null) return RedirectToAction("Index", "Leagues");
                ViewBag.CupId = id;
                ViewBag.CupName = name;
                var clubsToCup = _context.Clubs.Where(c => c.LeagueId == id).Include(c => c.League);
                return View(await clubsToCup.ToListAsync());
            }
            else if (typeTournament == "EuroCups")
            {
                if (id == null) return RedirectToAction("Index", "EuroCups");
                ViewBag.CupId = id;
                ViewBag.CupName = name;
                var clubsToCup = _context.Clubs.Where(c => c.EuroCupId == id).Include(c => c.EuroCup);
                return View(await clubsToCup.ToListAsync());
            }

            else return RedirectToAction("Index", typeTournament);
        }

        // GET: Clubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs
                .Include(c => c.Cup)
                .Include(c => c.EuroCup)
                .Include(c => c.League)
                .Include(c => c.National)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Footbollers", new { id = club.Id, name = club.Name, teamType = "club" });
        }

        // GET: Clubs/Create
        public IActionResult Create()
        {
            ViewData["CupId"] = new SelectList(_context.Cups, "Id", "Name");
            ViewData["EuroCupId"] = new SelectList(_context.EuroCups, "Id", "Name");
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name");
            ViewData["NationalId"] = new SelectList(_context.Nationals, "Id", "Name");
            return View();
        }

        // POST: Clubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PlayersNumber,DateOfBirth,TrophiesNumber,NationalId,PositionInLeague,CupId,LeagueId,EuroCupId")] Club club)
        {
            if (ModelState.IsValid)
            {
                _context.Add(club);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CupId"] = new SelectList(_context.Cups, "Id", "Name", club.CupId);
            ViewData["EuroCupId"] = new SelectList(_context.EuroCups, "Id", "Name", club.EuroCupId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", club.LeagueId);
            ViewData["NationalId"] = new SelectList(_context.Nationals, "Id", "Name", club.NationalId);
            return View(club);
        }

        // GET: Clubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            ViewData["CupId"] = new SelectList(_context.Cups, "Id", "Name", club.CupId);
            ViewData["EuroCupId"] = new SelectList(_context.EuroCups, "Id", "Name", club.EuroCupId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", club.LeagueId);
            ViewData["NationalId"] = new SelectList(_context.Nationals, "Id", "Name", club.NationalId);
            return View(club);
        }

        // POST: Clubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PlayersNumber,DateOfBirth,TrophiesNumber,NationalId,PositionInLeague,CupId,LeagueId,EuroCupId")] Club club)
        {
            if (id != club.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(club);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubExists(club.Id))
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
            ViewData["CupId"] = new SelectList(_context.Cups, "Id", "Name", club.CupId);
            ViewData["EuroCupId"] = new SelectList(_context.EuroCups, "Id", "Name", club.EuroCupId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", club.LeagueId);
            ViewData["NationalId"] = new SelectList(_context.Nationals, "Id", "Name", club.NationalId);
            return View(club);
        }

        // GET: Clubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs
                .Include(c => c.Cup)
                .Include(c => c.EuroCup)
                .Include(c => c.League)
                .Include(c => c.National)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var club = await _context.Clubs.FindAsync(id);
            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubExists(int id)
        {
            return _context.Clubs.Any(e => e.Id == id);
        }
    }
}
