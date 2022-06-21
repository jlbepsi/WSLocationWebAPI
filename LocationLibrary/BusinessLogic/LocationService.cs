using LocationLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationLibrary.BusinessLogic
{
    public class LocationService : ILocationService
    {
        private rhlocationContext contexte;

        public LocationService()
        {
            this.contexte = new rhlocationContext();
        }
        public LocationService(rhlocationContext contexte)
        {
            this.contexte = contexte;
        }

        public List<Location> GetLocations()
        {
            return contexte.Locations.Include(r => r.Reglements).ToList();
        }


        public Location GetLocation(int id)
        {
            return contexte.Locations.FirstOrDefault(l => l.Id == id);
        }

        public Location AddLocation(Location location)
        {
            // Vérification des dates
            location.CheckDate();

            Location locationDB = new Location()
            {
                IdUtilisateur = location.IdUtilisateur,
                IdHabitation = location.IdHabitation,
                Datedebut = location.Datedebut,
                Datefin = location.Datefin,
                Montant = location.Montant,
            };

            // Ajout de la location
            contexte.Locations.Add(locationDB);

            try
            {
                contexte.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new LocationException(string.Format("Erreur dans l'ajout de la location dans le référentiel", location.ToString()), ex);
            }
            return locationDB;
        }

        public Location DeleteLocation(int id)
        {
            // Ajout de la location
            Location location = contexte.Locations.FirstOrDefault(l => l.Id == id);
            if (location == null)
                return null;

            contexte.Locations.Remove(location);
            try
            {
                contexte.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // LogManager.GetLogger().Error(ex);
                throw new LocationException(string.Format("Erreur dans la suppression de la location dans le référentiel", location.ToString()), ex);
            }
            return location;
        }
    }
}
