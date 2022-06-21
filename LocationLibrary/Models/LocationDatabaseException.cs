using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationLibrary.Models
{
    public class LocationDatabaseException : LocationException
    {
        public LocationDatabaseException() : base("Base de données indisponible")
        {
            StatusCode = 503;
        }
    }
}
