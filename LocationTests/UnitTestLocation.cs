using LocationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationTests
{
    public class UnitTestLocation
    {
        private Location getDummyLocation()
        {
            return new Location()
            {
                Id = 1,
                IdUtilisateur = 1,
                IdHabitation = 3,
                Datedebut = new DateTime(2022, 7, 5),
                Datefin = new DateTime(2022, 7, 6),
                Montant = 100,
            };
        }

        private void LocationDateConstraintException()
        {
            // Arrange
            Location location = getDummyLocation();
            location.Datedebut = location.Datefin.AddDays(1);

            // Act
            location.CheckDate();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstraintOk_ShouldSuccess()
        {
            // Arrange
            Location location = getDummyLocation();

            // Act
            location.CheckDate();

            // Assert
            Assert.Pass();
        }

        [Test]
        public void ConstraintDateDebut_ShouldFail()
        {
            LocationException exception = Assert.Throws<LocationException>(
                delegate
                {
                    // Arrange
                    Location location = getDummyLocation();
                    location.Datedebut = DateTime.Now.AddDays(-1);

                    // Act
                    location.CheckDate();

                    // Assert : LocationException should be raised
                });

            // Assert
            Assert.That(exception.Message, Is.EqualTo("La date de début doit être après la date du jour"));
        }

        [Test]
        public void ConstraintDates_ShouldFail()
        {
            LocationException exception = Assert.Throws<LocationException>(
                delegate
                {
                    // Arrange
                    Location location = getDummyLocation();
                    location.Datedebut = location.Datefin.AddDays(1);

                    // Act
                    location.CheckDate();

                    // Assert : LocationException should be raised
                });

            // Assert
            Assert.That(exception.Message, Is.EqualTo("La date de fin doit être après la date de début"));
        }
    }
}
