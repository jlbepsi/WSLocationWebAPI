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
            mockService.Setup(x => x.GetLocations()).Returns(TestsUtilitaires.getDummyLocations());
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
            mockService.Setup(x => x.GetLocation(1)).Returns(TestsUtilitaires.getDummyLocation());
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
            Location location = TestsUtilitaires.getDummyLocation();
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
            Location location = TestsUtilitaires.getDummyLocation();

            var mockService = new Mock<ILocationService>();
            mockService.Setup(x => x.AddLocation(It.IsAny<Location>())).Throws(new LocationException("La date de fin doit être après la date de début"));
            var mockLogger = new Mock<ILogger<LocationsController>>();
            var controller = new LocationsController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = controller.AddLocation(location);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Result);
            var badRequest = actionResult.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.That(badRequest.Value, Is.EqualTo("La date de fin doit être après la date de début"));
        }

        [Test]
        public void PUTUpdate_ShouldReturnBadRequest()
        {
            // Should not be implemented

            // Arrange
            Location location = TestsUtilitaires.getDummyLocation();
            var mockService = new Mock<ILocationService>();
            var mockLogger = new Mock<ILogger<LocationsController>>();
            var controller = new LocationsController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = controller.UpdateLocation(1, location);

            // Assert
            Assert.IsNotNull(actionResult);
            var badRequest = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.That(badRequest.Value, Is.EqualTo("Non implémenté, vous devez supprimer puis créer une location"));

        }

        [Test]
        public void DELETEDelete_ShouldReturnOK()
        {
            // Arrange
            var mockService = new Mock<ILocationService>();
            mockService.Setup(x => x.DeleteLocation(It.IsAny<int>())).Returns(TestsUtilitaires.getDummyLocation());
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