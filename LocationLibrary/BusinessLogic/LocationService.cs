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
    private readonly rhlocationContext contexte;

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
        return contexte.Locations
            .Include(f => f.Facture)
            .Include(r => r.Reglements)
            .Include(r => r.Relances)
            .ToList();
    }


    public Location GetLocation(int id)
    {
        return contexte.Locations
            .Include(f => f.Facture)
            .Include(r => r.Reglements)
            .Include(r => r.Relances)
            .FirstOrDefault(l => l.Id == id);
    }

        public Location AddLocation(Location location)
        {
            // Vérification des contraintes
            location.CheckAll();

            Location locationDB = new Location()
            {
                Idutilisateur = location.Idutilisateur,
                Idhabitation = location.Idhabitation,
                Datedebut = location.Datedebut,
                Datefin = location.Datefin,
                Montanttotal = location.Montanttotal,
                Montantverse = location.Montantverse
            };

            // Ajout de la location
            contexte.Locations.Add(locationDB);

            try
            {
                contexte.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new LocationException($"Erreur dans l'ajout de la location dans le référentiel : {location.ToString()}", ex);
            }
            return locationDB;
        }

        public Location DeleteLocation(int id)
        {
            // Suppression de la location
            Location location = contexte.Locations.FirstOrDefault(l => l.Id == id);
            if (location == null)
                return null;

            // SI la date de début de location est avant la date du jour, la supression est possible
            // SINON on lance une exception
            if (location.Datedebut.CompareTo(DateTime.Now) >= 0)
            {
                throw new LocationException($"Suppression de la location impossible car la location a déjà commencé: {location.ToString()}");
            }

            contexte.Locations.Remove(location);
            try
            {
                contexte.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new LocationException($"Erreur dans la suppression de la location dans le référentiel : {location.ToString()}", ex);
            }
            return location;
        }
    }
}
