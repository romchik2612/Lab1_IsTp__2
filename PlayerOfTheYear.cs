using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1_IsTp__2
{
    public partial class PlayerOfTheYear
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WinnerId { get; set; }

        public virtual Footboller Winner { get; set; }
    }
}
