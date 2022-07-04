using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationLibrary.Models
{
    public static class LocationExtensions
    {
        public static string ToString(this Location location)
        {
            return $"Location ({location.Id}, User: {location.Idutilisateur}, Habitation: {location.Idhabitation}, " +
                $"de {location.Datedebut.ToShortDateString()} à {location.Datefin.ToShortDateString()}";
        }
    }
}
