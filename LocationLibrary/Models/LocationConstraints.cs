using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationLibrary.Models
{
    public static class LocationConstraints
    {
        public static void CheckDate(this Location location)
        {
            if (location.Datedebut.CompareTo(DateTime.Now) < 0)
            {
                throw new LocationException("La date de début doit être après la date du jour");
            }
            if (location.Datedebut.CompareTo(location.Datefin) >= 0)
            {
                throw new LocationException("La date de fin doit être après la date de début");
            }
        }
        public static void CheckMontant(this Location location)
        {
            if (location.Montantverse > location.Montanttotal)
            {
                throw new LocationException("Le montant versé ne peut pas être supérieur au montant total");
            }
        }
        public static void CheckAll(this Location location)
        {
            location.CheckDate();
            location.CheckMontant();
        }
    }
}
