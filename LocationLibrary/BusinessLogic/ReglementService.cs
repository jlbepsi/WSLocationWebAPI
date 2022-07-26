using LocationLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationLibrary.BusinessLogic
{
    public class ReglementService : IReglementService
    {
        private readonly rhlocationContext contexte;

        public ReglementService(rhlocationContext contexte)
        {
            this.contexte = contexte;
        }

        public List<Reglement> GetReglements()
        {
            return contexte.Reglements
                .Include(l => l.Location)
                .Include(t => t.Typereglement)
                .ToList();
        }

        public List<Reglement> GetReglementsByLocationId(int idLocation)
        {
            return contexte.Reglements
                .Include(l => l.Location)
                .Include(t => t.Typereglement)
                .Where(l => l.LocationId == idLocation)
                .ToList();
        }

        public Reglement GetReglement(int id)
        {
            return contexte.Reglements
                .Include(l => l.Location)
                .Include(t => t.Typereglement)
                .FirstOrDefault(r => r.Id == id);
        }

        public Reglement AddReglement(Reglement Reglement)
        {
            throw new NotImplementedException();
        }

        public Reglement DeleteReglement(int id)
        {
            throw new NotImplementedException();
        }
    }
}
