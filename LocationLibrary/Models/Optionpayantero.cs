using System;
using System.Collections.Generic;

namespace LocationLibrary.Models
{
    public partial class Optionpayantero
    {
        public Optionpayantero()
        {
            LocationOptionpayanteros = new HashSet<LocationOptionpayantero>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LocationOptionpayantero> LocationOptionpayanteros { get; set; }
    }
}
