using System.Collections.Generic;
using LocationLibrary.Models;

namespace LocationLibrary.BusinessLogic
{
    public interface ILocationService
    {
        List<Location> GetLocations();
        Location GetLocation(int id);
        Location AddLocation(Location location);
        Location DeleteLocation(int id);
    }
}