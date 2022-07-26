using LocationLibrary.BusinessLogic;
using LocationLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LocationTests;

public class TestLocationService
{
  // private static int month = 7, dayStart = 1, dayEnd = 4;
  private static IQueryable<Location> dummyLocations;
  private Mock<DbSet<Location>> mockDbSetLocation = new Mock<DbSet<Location>>();

  public TestLocationService()
  {
    dummyLocations = new List<Location> {
      new Location()
      {
        Id = 1,
        Idutilisateur = 1,
        Idhabitation = 3,
        Datedebut = new DateTime(2048, 7, 10),
        Datefin = new DateTime(2048, 7, 14),
        Montanttotal = 100,
        Montantverse = 100,
      },
      new Location()
      {
        Id = 2,
        Idutilisateur = 2,
        Idhabitation = 4,
        Datedebut = new DateTime(2048, 7, 10),
        Datefin = new DateTime(2048, 7, 11),
        Montanttotal = 100,
        Montantverse = 100,
      },
    }.AsQueryable();
  }
  
  [SetUp]
  public void Setup()
  {
    mockDbSetLocation.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(dummyLocations.Provider);
    mockDbSetLocation.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(dummyLocations.Expression);
    mockDbSetLocation.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(dummyLocations.ElementType);
    mockDbSetLocation.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(dummyLocations.GetEnumerator());
  }

  [Test]
  public void GetAll_ShouldReturnOk()
  {
    // Arrange
    var mockContext = new Mock<rhlocationContext>();
    mockContext.Setup(c => c.Locations).Returns(mockDbSetLocation.Object);
    var service = new LocationService(mockContext.Object);

    // Act
    var locations = service.GetLocations();
    
    // Assert
    Assert.That(locations.Count, Is.EqualTo(2));
  }

  [Test]
  public void GetOne_ShouldReturnOk()
  {
    // Arrange
    var mockContext = new Mock<rhlocationContext>();
    mockContext.Setup(c => c.Locations).Returns(mockDbSetLocation.Object);
    var service = new LocationService(mockContext.Object);

    // Act
    var location = service.GetLocation(1);
    
    // Assert
    Assert.IsNotNull(location);
    Assert.That(location.Id, Is.EqualTo(1));
  }

  [Test]
  public void Add_ShouldReturnCreated()
  {
    // Arrange
    var mockContext = new Mock<rhlocationContext>();
    mockContext.Setup(c => c.Locations).Returns(mockDbSetLocation.Object);
    var service = new LocationService(mockContext.Object);
    var locationToAdd = new Location()
    {
      Id = 1,
      Idutilisateur = 1,
      Idhabitation = 3,
      Datedebut = new DateTime(2048, 7, 5),
      Datefin = new DateTime(2048, 7, 7),
      Montanttotal = 100,
      Montantverse = 100,
    };

    // Act
    var location = service.AddLocation(locationToAdd);
    
    // Assert
    Assert.IsNotNull(location);
    //Assert.That(location.Id, Is.EqualTo(1));
  }

  [Test, Sequential]
  public void AddExistingLocation_ShouldThrowException(
    [Values(10, 11, 9, 13, 1)] int dayStart, 
    [Values(14, 13, 11, 15, 15)] int dayEnd)
  {
    // Arrange
    var mockContext = new Mock<rhlocationContext>();
    mockContext.Setup(c => c.Locations).Returns(mockDbSetLocation.Object);
    var service = new LocationService(mockContext.Object);
    var locationToAdd = new Location()
    {
      Id = 100,
      Idutilisateur = 1,
      Idhabitation = 3,
      Datedebut = new DateTime(2048, 7, dayStart),
      Datefin = new DateTime(2048, 7, dayEnd),
      Montanttotal = 100,
      Montantverse = 100,
    };
    
    LocationException exception = Assert.Throws<LocationException>(delegate
      { 
        // Act
        service.AddLocation(locationToAdd);
      });
    
    // Assert
    Assert.That(exception.Message, Is.EqualTo("Une location existe déjà pour les dates demandées"));
  }
}