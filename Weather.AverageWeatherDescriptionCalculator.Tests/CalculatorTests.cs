
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

        [TestCase(TypeWindDirection.North, TypeWindDirection.East, TypeWindDirection.NorthEast)]
        [TestCase(TypeWindDirection.East, TypeWindDirection.South, TypeWindDirection.SouthEast)]
        [TestCase(TypeWindDirection.South, TypeWindDirection.West, TypeWindDirection.SouthWest)]
        [TestCase(TypeWindDirection.West, TypeWindDirection.North, TypeWindDirection.NorthWest)]
        [Test]
        public void GetAvgWeatherDescription_CalculateAverageDirectionEveryFourthWithSameSpeed_AverageDirection(
            TypeWindDirection direction1, TypeWindDirection direction2, TypeWindDirection result)
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescription { WindDirection = direction1, WindSpeed = 10 }, 
                new WeatherDescription { WindDirection = direction2, WindSpeed = 10 }
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindDirection, result);
        }

        [TestCase(TypeWindDirection.North, TypeWindDirection.West, TypeWindDirection.NorthWest)]
        [TestCase(TypeWindDirection.North, TypeWindDirection.East, TypeWindDirection.NorthEast)]
        [Test]
        public void GetAvgWeatherDescription_CalculateAverageDirectionNearZerroWithSameSpeed_AverageDirection(
            TypeWindDirection direction1, TypeWindDirection direction2, TypeWindDirection result)
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescription { WindDirection = direction1, WindSpeed = 10 }, 
                new WeatherDescription { WindDirection = direction2, WindSpeed = 10 }
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
                new WeatherDescription { WindDirection = TypeWindDirection.West, WindSpeed = 0 }, 
                new WeatherDescription { WindDirection = TypeWindDirection.North, WindSpeed = 20 },
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindDirection, TypeWindDirection.North);
        }

        [TestCase(TypeWindDirection.North, TypeWindDirection.South)]
        [TestCase(TypeWindDirection.NorthEast, TypeWindDirection.SouthWest)]
        [TestCase(TypeWindDirection.East, TypeWindDirection.West)]
        [TestCase(TypeWindDirection.SouthEast, TypeWindDirection.NorthWest)]
        [TestCase(TypeWindDirection.South, TypeWindDirection.North)]
        [Test]
        public void GetAvgWeatherDescription_CalculateAverageDirectionWithCrosswindAndDifferentSpeed_DirectionWhereMoreSpeed(
            TypeWindDirection direction1, TypeWindDirection direction2)
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescription { WindDirection = direction1, WindSpeed = 10 }, 
                new WeatherDescription { WindDirection = direction2, WindSpeed = 11 }
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
                new WeatherDescription { WindDirection = TypeWindDirection.North, WindSpeed = 0 }, 
                new WeatherDescription { WindDirection = TypeWindDirection.East, WindSpeed = 20 },
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindSpeed, 10);
        }

        [TestCase(TypeWindDirection.North, TypeWindDirection.South, 10, 11)]
        [TestCase(TypeWindDirection.NorthEast, TypeWindDirection.SouthWest, 10, 16)]
        [TestCase(TypeWindDirection.East, TypeWindDirection.West, 2, 1)]
        [TestCase(TypeWindDirection.SouthEast, TypeWindDirection.NorthWest, 17, 8)]
        [TestCase(TypeWindDirection.South, TypeWindDirection.North, 5, 4)]
        [Test]
        public void GetAvgWeatherDescription_CalculateAverageSpeedWithCrosswindAndDifferentSpeed_AverageSpeed(
            TypeWindDirection direction1, TypeWindDirection direction2, double speed1, double speed2)
        {
            // Arrange
            var weatherDescription = new[]
            {
                new WeatherDescription { WindDirection = direction1, WindSpeed = speed1 }, 
                new WeatherDescription { WindDirection = direction2, WindSpeed = speed2 }
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
                new WeatherDescription { WindDirection = TypeWindDirection.South, WindSpeed = 20 }, 
                new WeatherDescription { WindDirection = TypeWindDirection.North, WindSpeed = 21 },
                new WeatherDescription { WindDirection = TypeWindDirection.North, WindSpeed = 2 }
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
                new WeatherDescription { WindDirection = TypeWindDirection.North, WindSpeed = 10 }, 
                new WeatherDescription { WindDirection = TypeWindDirection.South, WindSpeed = 10 },
                new WeatherDescription { WindDirection = TypeWindDirection.East, WindSpeed = 20 }, 
                new WeatherDescription { WindDirection = TypeWindDirection.West, WindSpeed = 20 }
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
                new WeatherDescription { WindDirection = TypeWindDirection.North, WindSpeed = 10 }, 
                new WeatherDescription { WindDirection = TypeWindDirection.South, WindSpeed = 10 },
                new WeatherDescription { WindDirection = TypeWindDirection.NorthWest, WindSpeed = 20 }, 
                new WeatherDescription { WindDirection = TypeWindDirection.SouthEast, WindSpeed = 20 },
                new WeatherDescription { WindDirection = TypeWindDirection.West, WindSpeed = 5 }
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
                new WeatherDescription { WindDirection = TypeWindDirection.North, WindSpeed = 10 }, 
                new WeatherDescription { WindDirection = TypeWindDirection.South, WindSpeed = 10 },
                new WeatherDescription { WindDirection = TypeWindDirection.North, WindSpeed = 20 }, 
                new WeatherDescription { WindDirection = TypeWindDirection.South, WindSpeed = 20 },
            };

            // Act
            var averageWeatherDescription = this.calculator.GetAvgWeatherDescription(weatherDescription);

            // Assert
            Assert.AreEqual(averageWeatherDescription.WindSpeed, 0);
        }
    }
}
