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
    public class NationFController : ControllerBase
    {
        private readonly FootballContext _context;

        public NationFController(FootballContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var nations = _context.Nationals.Include(m => m.Footbollers).ToList();
            List<object> nFootboller = new List<object>();
            nFootboller.Add(new[] { "League", "Active Clubs Number" });
            foreach (var m in nations)
            {
                nFootboller.Add(new object[] { m.Name, m.Footbollers.Count() });
            }
            return new JsonResult(nFootboller);




        }
    }
}
