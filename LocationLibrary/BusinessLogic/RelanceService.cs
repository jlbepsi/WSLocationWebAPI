using LocationLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationLibrary.BusinessLogic
{
    public class RelanceService : IRelanceService
    {
        private readonly rhlocationContext contexte;

        public RelanceService()
        {
            this.contexte = new rhlocationContext();
        }
        public RelanceService(rhlocationContext contexte)
        {
            this.contexte = contexte;
        }

        public List<Relance> GetRelances()
        {
            return contexte.Relances
                .Include(l => l.Location)
                .ToList();
        }

        public Relance GetRelance(int id)
        {
            return contexte.Relances
                .Include(l => l.Location)
                .FirstOrDefault(r => r.Id == id);
        }

        public Relance AddRelance(Relance relance)
        {
            throw new NotImplementedException();
        }

        public Relance DeleteRelance(int id)
        {
            throw new NotImplementedException();
        }
    }
}
