using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocationLibrary.Models
{
    public partial class Reglement
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        [Range(10, Int32.MaxValue, ErrorMessage = "Le montant miniumn est de 10 euros")]
        public decimal Montant { get; set; }
        public DateTime Dateversement { get; set; }
        public int TypereglementId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Typereglement Typereglement { get; set; }
    }
}
