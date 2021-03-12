using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab1_IsTp__2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {

        private readonly FootballContext _context;

        public ChartsController(FootballContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var cups = _context.Cups.Include(m => m.Clubs).ToList();
            List<object> fClub = new List<object>();
            fClub.Add(new[] { "League", "Active Clubs Number" });
            foreach (var m in cups) {
                fClub.Add(new object[] { m.Name, m.Clubs.Count() });
            }
            return new JsonResult(fClub);
        
        
        
        
        }









    }
}
