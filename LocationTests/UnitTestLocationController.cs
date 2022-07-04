using System;
using System.Collections.Generic;
using LocationLibrary.BusinessLogic;
using LocationLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WSLocationWebAPI.Controllers;

namespace LocationTests
{
    public class UnitTestLocationController
    {
        private List<Location> getDummyLocations()
        {
            return new List<Location> {
                new Location()
                {
                    Id = 1,
                    Idutilisateur = 1,
                    Idhabitation = 3,
                    Datedebut = new DateTime(2022, 7, 5),
                    Datefin = new DateTime(2022, 7, 6),
                    Montanttotal = 100,
                    Montantverse = 100,
                },
                new Location()
                {
                    Id = 2,
                    Idutilisateur = 2,
                    Idhabitation = 4,
                    Datedebut = new DateTime(2022, 7, 12),
                    Datefin = new DateTime(2022, 7, 17),
                    Montanttotal = 420,
                    Montantverse = 100,
                }
            };
        }
        private Location getDummyLocation()
        {
            return new Location()
            {
                Id = 1,
                Idutilisateur = 1,
                Idhabitation = 3,
                Datedebut = new DateTime(2022, 7, 5),
                Datefin = new DateTime(2022, 7, 6),
                Montanttotal = 100,
                Montantverse = 100,
            };
        }


        /*
        [SetUp]
        public void Setup()
        {
        }
        */

        [Test]
        public void GetAll_ReturnsOK()
        {
            // Arrange
            var mockService = new Mock<ILocationService>();
            mockService.Setup(x => x.GetLocations()).Returns(getDummyLocations());
            var mockLogger = new Mock<ILogger<LocationsController>> ();
            var controller = new LocationsController(mockService.Object, mockLogger.Object);

            // Act
            var result = controller.GetLocations();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.That(result.Value.Count, Is.EqualTo(2));
            Assert.That(result.Value[0].Id, Is.EqualTo(1));

        }

        [Test]
        public void GetOne_ReturnsOK()
        {
            // Arrange
            var mockService = new Mock<ILocationService>();
            mockService.Setup(x => x.GetLocation(1)).Returns(getDummyLocation());
            var mockLogger = new Mock<ILogger<LocationsController>>();
            var controller = new LocationsController(mockService.Object, mockLogger.Object);

            // Act
            var result = controller.GetLocation(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.That(result.Value.Id, Is.EqualTo(1));

        }

        [Test]
        public void POSTCreate_ShouldReturnCreated()
        {
            // Arrange
            Location location = getDummyLocation();
            var mockService = new Mock<ILocationService>();
            mockService.Setup(x => x.AddLocation(It.IsAny<Location>())).Returns(location);
            var mockLogger = new Mock<ILogger<LocationsController>>();
            var controller = new LocationsController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = controller.AddLocation(location);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Result);
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(result);

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
            Assert.IsNotNull(result.Value);
            Location? locationCreated = result.Value as Location;
            Assert.IsNotNull(locationCreated);
            Assert.That(locationCreated.Id, Is.EqualTo(1));
        }

        [Test]
        public void POSTCreateConstraintBadDates_ShouldReturnBadRequest()
        {
            // Arrange
            Location location = getDummyLocation();

            var mockService = new Mock<ILocationService>();
            mockService.Setup(x => x.AddLocation(It.IsAny<Location>())).Throws(new LocationException("La date de fin doit �tre apr�s la date de d�but"));
            var mockLogger = new Mock<ILogger<LocationsController>>();
            var controller = new LocationsController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = controller.AddLocation(location);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Result);
            var badRequest = actionResult.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.That(badRequest.Value, Is.EqualTo("La date de fin doit �tre apr�s la date de d�but"));
        }

        [Test]
        public void PUTUpdate_ShouldReturnBadRequest()
        {
            // Should not be implemented

            // Arrange
            Location location = getDummyLocation();
            var mockService = new Mock<ILocationService>();
            var mockLogger = new Mock<ILogger<LocationsController>>();
            var controller = new LocationsController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = controller.UpdateLocation(1, location);

            // Assert
            Assert.IsNotNull(actionResult);
            var badRequest = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.That(badRequest.Value, Is.EqualTo("Non impl�ment�, vous devez supprimer puis cr�er une location"));

        }

        [Test]
        public void DELETEDelete_ShouldReturnOK()
        {
            // Arrange
            var mockService = new Mock<ILocationService>();
            mockService.Setup(x => x.DeleteLocation(It.IsAny<int>())).Returns(getDummyLocation());
            var mockLogger = new Mock<ILogger<LocationsController>>();
            var controller = new LocationsController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = controller.DeleteLocation(1);

            // Assert
            Assert.IsNotNull(actionResult);
            var ok = actionResult as OkObjectResult;
            Assert.IsNotNull(ok);
            Assert.That(ok.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public void DELETEDeleteUnknown_ShouldReturnNotFound()
        {
            // Arrange
            var mockService = new Mock<ILocationService>();
            mockService.Setup(x => x.DeleteLocation(It.IsAny<int>())).Returns(value: null);
            var mockLogger = new Mock<ILogger<LocationsController>>();
            var controller = new LocationsController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = controller.DeleteLocation(1);

            // Assert
            Assert.IsNotNull(actionResult);
            var notFound = actionResult as NotFoundResult;
            Assert.IsNotNull(notFound);
        }
    }
}