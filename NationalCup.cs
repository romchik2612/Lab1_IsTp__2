using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1_IsTp__2
{
    public partial class NationalCup
    {
        public NationalCup()
        {
            Nationals = new HashSet<National>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamsNumber { get; set; }

        public virtual ICollection<National> Nationals { get; set; }
    }
}
