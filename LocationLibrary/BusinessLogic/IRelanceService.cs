using System.Collections.Generic;
using LocationLibrary.Models;

namespace LocationLibrary.BusinessLogic
{
    public interface IRelanceService
    {
        List<Relance> GetRelances();
        List<Relance> GetRelancesByLocationId(int idLocation);
        Relance GetRelance(int id);
        Relance AddRelance(Relance relance);
        Relance DeleteRelance(int id);
    }
}