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
        /*
        [SetUp]
        public void Setup()
        {
        }
        */

        [Test]
        public void ConstraintOk_ShouldSuccess()
        {
            // Arrange
            Location location = TestsUtilitaires.getDummyLocation();

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
                    Location location = TestsUtilitaires.getDummyLocation();
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
                    Location location = TestsUtilitaires.getDummyLocation();
                    location.Datedebut = location.Datefin.AddDays(1);

                    // Act
                    location.CheckDate();

                    // Assert : LocationException should be raised
                });

            // Assert
            Assert.That(exception.Message, Is.EqualTo("La date de fin doit être après la date de début"));
        }

        [Test]
        public void ConstraintMontants_ShouldFail()
        {
            LocationException exception = Assert.Throws<LocationException>(
                delegate
                {
                    // Arrange
                    Location location = TestsUtilitaires.getDummyLocation();
                    location.Montanttotal = 100;
                    location.Montantverse = location.Montanttotal + 1;

                    // Act
                    location.CheckMontant();

                    // Assert : LocationException should be raised

                });

            // Assert
            Assert.That(exception.Message, Is.EqualTo("Le montant versé ne peut pas être supérieur au montant total"));
        }
    }
}
