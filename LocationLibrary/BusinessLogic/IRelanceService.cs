﻿using LocationLibrary.Models;

namespace LocationLibrary.BusinessLogic
{
    public interface IRelanceService
    {
        List<Relance> GetRelances();
        Relance GetRelance(int id);
        Relance AddRelance(Relance relance);
        Relance DeleteRelance(int id);
    }
}