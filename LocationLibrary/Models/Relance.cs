using System;
using System.Collections.Generic;

namespace LocationLibrary.Models
{
    public partial class Relance
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public string Motif { get; set; } = null!;

        public virtual Location Location { get; set; } = null!;
    }
}
