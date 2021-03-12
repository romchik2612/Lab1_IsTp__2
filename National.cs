using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1_IsTp__2
{
    public partial class National
    {
        public National()
        {
            Clubs = new HashSet<Club>();
            Footbollers = new HashSet<Footboller>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TrophiesNumber { get; set; }
        public int NationalCupId { get; set; }

        public virtual NationalCup NationalCup { get; set; }
        public virtual ICollection<Club> Clubs { get; set; }
        public virtual ICollection<Footboller> Footbollers { get; set; }
    }
}
