using System;
using System.Collections.Generic;

namespace LocationLibrary.Models
{
    public partial class Typereglement
    {
        public Typereglement()
        {
            // Reglements = new HashSet<Reglement>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }

        // public virtual ICollection<Reglement> Reglements { get; set; }
    }
}
