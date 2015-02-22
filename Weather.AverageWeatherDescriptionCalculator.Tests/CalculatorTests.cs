
using System;

using NUnit.Framework;

using Weather.AverageWeatherDescriptionCalculator.Interfaces;
using Weather.Common.Entities;

namespace Weather.AverageWeatherDescriptionCalculator.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private ICalculator calculator;

        [TestFixtureSetUp]
        public void Initialize()
        {
            this.calculator = new Calculator();
        }

        [TestCase(WindDirectionTypeEntity.North, WindDirectionTypeEntity.East, WindDirectionTypeEntity.NorthEast)]
        [TestCase(WindDirectionTypeEntity.East, WindDirectionTypeEntity.South, WindDirectionTypeEntity.SouthEast)]
        [TestCase(WindDirectionTypeEntity.South, WindDirectionTypeEntity.West, WindDirectionTypeEntity.SouthWest)]
        [TestCase(WindDirectionTypeEntity.West, WindDirectionTypeEntity.North, WindDirectionTypeEntity.NorthWest)]
        [Test]
        public void GetAvgWeatherDescription_CalculateAverageDirectionEveryFourthWithSameSpeed_AverageDirection(
            WindDirectionTypeEntity direction1, WindDirectionTypeEntity direction2, WindDirectionTypeEntity result)
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = direction1, WindSpeed = 10 }, 
                new WeatherDescriptionEntity { WindDirection = direction2, WindSpeed = 10 }
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindDirection, result);
        }

        [TestCase(WindDirectionTypeEntity.North, WindDirectionTypeEntity.West, WindDirectionTypeEntity.NorthWest)]
        [TestCase(WindDirectionTypeEntity.North, WindDirectionTypeEntity.East, WindDirectionTypeEntity.NorthEast)]
        [Test]
        public void GetAvgWeatherDescription_CalculateAverageDirectionNearZerroWithSameSpeed_AverageDirection(
            WindDirectionTypeEntity direction1, WindDirectionTypeEntity direction2, WindDirectionTypeEntity result)
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = direction1, WindSpeed = 10 }, 
                new WeatherDescriptionEntity { WindDirection = direction2, WindSpeed = 10 }
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);
            
            // Assert
            Assert.AreEqual(averageWeatherDescription.WindDirection, result);
        }

        [Test]
        public void GetAvgWeatherDescription_CalculateAverageDirectionWithCalm_DirectionWhereSpeedNotZerro()
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.West, WindSpeed = 0 }, 
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.North, WindSpeed = 20 },
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindDirection, WindDirectionTypeEntity.North);
        }

        [TestCase(WindDirectionTypeEntity.North, WindDirectionTypeEntity.South)]
        [TestCase(WindDirectionTypeEntity.NorthEast, WindDirectionTypeEntity.SouthWest)]
        [TestCase(WindDirectionTypeEntity.East, WindDirectionTypeEntity.West)]
        [TestCase(WindDirectionTypeEntity.SouthEast, WindDirectionTypeEntity.NorthWest)]
        [TestCase(WindDirectionTypeEntity.South, WindDirectionTypeEntity.North)]
        [Test]
        public void GetAvgWeatherDescription_CalculateAverageDirectionWithCrosswindAndDifferentSpeed_DirectionWhereMoreSpeed(
            WindDirectionTypeEntity direction1, WindDirectionTypeEntity direction2)
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = direction1, WindSpeed = 10 }, 
                new WeatherDescriptionEntity { WindDirection = direction2, WindSpeed = 11 }
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindDirection, direction2);
        }

        [Test]
        public void GetAvgWeatherDescription_CalculateAverageSpeedWithCalm_AverageSpeedHowArithmeticAverage()
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.North, WindSpeed = 0 }, 
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.East, WindSpeed = 20 },
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindSpeed, 10);
        }

        [TestCase(WindDirectionTypeEntity.North, WindDirectionTypeEntity.South, 10, 11)]
        [TestCase(WindDirectionTypeEntity.NorthEast, WindDirectionTypeEntity.SouthWest, 10, 16)]
        [TestCase(WindDirectionTypeEntity.East, WindDirectionTypeEntity.West, 2, 1)]
        [TestCase(WindDirectionTypeEntity.SouthEast, WindDirectionTypeEntity.NorthWest, 17, 8)]
        [TestCase(WindDirectionTypeEntity.South, WindDirectionTypeEntity.North, 5, 4)]
        [Test]
        public void GetAvgWeatherDescription_CalculateAverageSpeedWithCrosswindAndDifferentSpeed_AverageSpeed(
            WindDirectionTypeEntity direction1, WindDirectionTypeEntity direction2, double speed1, double speed2)
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = direction1, WindSpeed = speed1 }, 
                new WeatherDescriptionEntity { WindDirection = direction2, WindSpeed = speed2 }
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindSpeed, Math.Round((speed1 > speed2 ? speed1 - speed2 : speed2 - speed1) / 2));
        }

        [Test]
        public void GetAvgWeatherDescription_CalculateAverageSpeedWithCrosswindWhereThreeItems_AverageSpeed()
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.South, WindSpeed = 20 }, 
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.North, WindSpeed = 21 },
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.North, WindSpeed = 2 }
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindSpeed, 1);
        }

        [Test]
        public void GetAvgWeatherDescription_CalculateCalmWithCrosswind_CalmTrue()
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.North, WindSpeed = 10 }, 
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.South, WindSpeed = 10 },
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.East, WindSpeed = 20 }, 
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.West, WindSpeed = 20 }
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindSpeed, 0);
        }

        [Test]
        public void GetAvgWeatherDescription_CalculateCalmWithCrosswindAndNotCrosswind_CalmFalse()
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.North, WindSpeed = 10 }, 
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.South, WindSpeed = 10 },
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.NorthWest, WindSpeed = 20 }, 
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.SouthEast, WindSpeed = 20 },
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.West, WindSpeed = 5 }
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreNotEqual(averageWeatherDescription.WindSpeed, 0);
        }

        [Test]
        public void GetAvgWeatherDescription_CalculateCalmWith0DegreeAnd360Degree_CalmTrue()
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.North, WindSpeed = 10 }, 
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.South, WindSpeed = 10 },
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.North, WindSpeed = 20 }, 
                new WeatherDescriptionEntity { WindDirection = WindDirectionTypeEntity.South, WindSpeed = 20 },
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindSpeed, 0);
        }
    }
}
