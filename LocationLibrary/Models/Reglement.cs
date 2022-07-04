using System;
using System.Collections.Generic;

namespace LocationLibrary.Models
{
    public partial class Reglement
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public decimal Montant { get; set; }
        public DateTime Dateversement { get; set; }
        public int TypereglementId { get; set; }

        public virtual Location Location { get; set; } = null!;
        public virtual Typereglement Typereglement { get; set; } = null!;
    }
}
