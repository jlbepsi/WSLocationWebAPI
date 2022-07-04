using System;
using System.Collections.Generic;

namespace LocationLibrary.Models
{
    public partial class Location
    {
        public Location()
        {
            Reglements = new HashSet<Reglement>();
            Relances = new HashSet<Relance>();
        }

        public int Id { get; set; }
        public int Idutilisateur { get; set; }
        public int Idhabitation { get; set; }
        public int? FactureId { get; set; }
        public DateTime Datedebut { get; set; }
        public DateTime Datefin { get; set; }
        public double Montanttotal { get; set; }
        public double Montantverse { get; set; }

        public virtual Facture? Facture { get; set; }
        public virtual ICollection<Reglement> Reglements { get; set; }
        public virtual ICollection<Relance> Relances { get; set; }
    }
}
