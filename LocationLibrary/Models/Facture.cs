using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocationLibrary.Models
{
    public partial class Facture
    {
        /// <summary>
        /// Doit être identique à location_id
        /// </summary>
        public int Id { get; set; }
        public DateTime Date { get; set; }
        
        [MinLength(5)]
        public string Adresse { get; set; }

        public virtual Location IdLocation { get; set; }
    }
}
