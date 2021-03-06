using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1_IsTp__2
{
    public partial class League
    {
        public League()
        {
            Clubs = new HashSet<Club>();
        }

        public int Id { get; set; }
        public int ClubsNumber { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Club> Clubs { get; set; }
    }
}
