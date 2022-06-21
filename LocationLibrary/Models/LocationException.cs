using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationLibrary.Models
{
    public class LocationException : Exception
    {
        public int StatusCode { get; set; }

        public LocationException() : base() { }
        public LocationException(string message) : base(message) { }
        public LocationException(string message, Exception ex) : base(message, ex) { }
    }
}
