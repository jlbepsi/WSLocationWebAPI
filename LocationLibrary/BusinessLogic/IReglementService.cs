using System.Collections.Generic;
using LocationLibrary.Models;

namespace LocationLibrary.BusinessLogic
{
    public interface IReglementService
    {
        List<Reglement> GetReglements();
        Reglement GetReglement(int id);
        Reglement AddReglement(Reglement Reglement);
        Reglement DeleteReglement(int id);
    }
}