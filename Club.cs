using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1_IsTp__2
{
    public partial class Club
    {
        public Club()
        {
            Footbollers = new HashSet<Footboller>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int PlayersNumber { get; set; }
        public string DateOfBirth { get; set; }
        public int TrophiesNumber { get; set; }
        public int NationalId { get; set; }
        public int PositionInLeague { get; set; }
        public int CupId { get; set; }
        public int LeagueId { get; set; }
        public int? EuroCupId { get; set; }

        public virtual Cup Cup { get; set; }
        public virtual EuroCup EuroCup { get; set; }
        public virtual League League { get; set; }
        public virtual National National { get; set; }
        public virtual ICollection<Footboller> Footbollers { get; set; }
    }
}
