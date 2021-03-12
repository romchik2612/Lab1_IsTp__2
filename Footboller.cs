using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1_IsTp__2
{
    public partial class Footboller
    {
        public Footboller()
        {
            PlayerOfTheYears = new HashSet<PlayerOfTheYear>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sourname { get; set; }
        public string Position { get; set; }
        public int NationalId { get; set; }
        public int ClubId { get; set; }
        public int? GoalsNumber { get; set; }
        public int? AssistsNumber { get; set; }
        public string DateOfBirth { get; set; }
        public int MatchesNumber { get; set; }

        public virtual Club Club { get; set; }
        public virtual National National { get; set; }
        public virtual ICollection<PlayerOfTheYear> PlayerOfTheYears { get; set; }
    }
}
