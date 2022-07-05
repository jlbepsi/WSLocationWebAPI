using LocationLibrary.Models;

namespace LocationTests;

public static class TestsUtilitaires
{
  public static Location getDummyLocation()
  {
    return new Location()
    {
      Id = 1,
      Idutilisateur = 1,
      Idhabitation = 3,
      Datedebut = DateTime.Now.AddDays(2),
      Datefin = DateTime.Now.AddDays(4),
      Montanttotal = 100,
      Montantverse = 100
    };
  }
  public static List<Location> getDummyLocations()
  {
    return new List<Location> {
      new Location()
      {
        Id = 1,
        Idutilisateur = 1,
        Idhabitation = 3,
        Datedebut = DateTime.Now.AddDays(2),
        Datefin = DateTime.Now.AddDays(3),
        Montanttotal = 100,
        Montantverse = 100,
      },
      new Location()
      {
        Id = 2,
        Idutilisateur = 2,
        Idhabitation = 4,
        Datedebut = DateTime.Now.AddDays(2),
        Datefin = DateTime.Now.AddDays(7),
        Montanttotal = 420,
        Montantverse = 100,
      }
    };
  }
}