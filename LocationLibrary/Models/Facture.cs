using System;
using System.Collections.Generic;

namespace LocationLibrary.Models
{
    public partial class Facture
    {
        public Facture()
        {
            Locations = new HashSet<Location>();
        }

        /// <summary>
        /// Doit être identique à location_id
        /// </summary>
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Adresse { get; set; } = null!;

        public virtual ICollection<Location> Locations { get; set; }
    }
}
