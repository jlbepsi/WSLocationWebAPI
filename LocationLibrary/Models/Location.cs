using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocationLibrary.Models
{
    public partial class Location
    {
        public Location()
        {
            Reglements = new HashSet<Reglement>();
        }

        public int Id { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdHabitation { get; set; }
        public DateTime Datedebut { get; set; }
        public DateTime Datefin { get; set; }
        [Range(10, double.MaxValue, ErrorMessage = "Le prix doit être supérieur ou égal à 10")]
        public decimal? Montant { get; set; }

        public virtual ICollection<Reglement> Reglements { get; set; }
    }
}
