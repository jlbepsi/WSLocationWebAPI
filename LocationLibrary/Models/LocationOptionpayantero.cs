using System;
using System.Collections.Generic;

namespace LocationLibrary.Models
{
    public partial class LocationOptionpayantero
    {
        public int LocationId { get; set; }
        public int OptionpayanteroId { get; set; }
        public double Prix { get; set; }

        public virtual Location Location { get; set; }
        public virtual Optionpayantero Optionpayantero { get; set; }
    }
}
