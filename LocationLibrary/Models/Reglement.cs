using System;
using System.Collections.Generic;

namespace LocationLibrary.Models
{
    public partial class Reglement
    {
        public int Id { get; set; }
        public int IdLocation { get; set; }
        public decimal Montant { get; set; }
        public DateTime Dateversement { get; set; }

        public virtual Location IdLocationNavigation { get; set; } = null!;
    }
}
