
using System;

using NUnit.Framework;

using Weather.AverageWeatherDataCalculator.Interfaces;
using Weather.Common.Dto;
using Weather.Common.Enums;

namespace Weather.AverageWeatherDataCalculator.Tests
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

        [Test]
        public void GetAvgWeatherData_CalculateAverageDirectionAnyNull_AverageDirection()
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = WindDirection.West, WindSpeed = 1 }, 
                new WeatherDataDto { WindDirection = null, WindSpeed = 0 },
                new WeatherDataDto { WindDirection = null, WindSpeed = 0 }
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindDirection, WindDirection.West);
        }

        [Test]
        public void GetAvgWeatherData_CalculateAverageDirectionAllNull_AverageDirection()
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = null, WindSpeed = 0 },
                new WeatherDataDto { WindDirection = null, WindSpeed = 0 }
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindDirection, null);
        }

        [TestCase(WindDirection.North, WindDirection.East, WindDirection.NorthEast)]
        [TestCase(WindDirection.East, WindDirection.South, WindDirection.SouthEast)]
        [TestCase(WindDirection.South, WindDirection.West, WindDirection.SouthWest)]
        [TestCase(WindDirection.West, WindDirection.North, WindDirection.NorthWest)]
        [Test]
        public void GetAvgWeatherData_CalculateAverageDirectionEveryFourthWithSameSpeed_AverageDirection(
            WindDirection direction1, WindDirection direction2, WindDirection result)
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = direction1, WindSpeed = 10 }, 
                new WeatherDataDto { WindDirection = direction2, WindSpeed = 10 }
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindDirection, result);
        }

        [TestCase(WindDirection.North, WindDirection.West, WindDirection.NorthWest)]
        [TestCase(WindDirection.North, WindDirection.East, WindDirection.NorthEast)]
        [Test]
        public void GetAvgWeatherData_CalculateAverageDirectionNearZerroWithSameSpeed_AverageDirection(
            WindDirection direction1, WindDirection direction2, WindDirection result)
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = direction1, WindSpeed = 10 }, 
                new WeatherDataDto { WindDirection = direction2, WindSpeed = 10 }
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);
            
            // Assert
            Assert.AreEqual(averageWeatherData.WindDirection, result);
        }

        [Test]
        public void GetAvgWeatherData_CalculateAverageDirectionWithCalm_DirectionWhereSpeedNotZerro()
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = null, WindSpeed = 0 }, 
                new WeatherDataDto { WindDirection = WindDirection.North, WindSpeed = 20 },
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindDirection, WindDirection.North);
        }

        [TestCase(WindDirection.North, WindDirection.South)]
        [TestCase(WindDirection.NorthEast, WindDirection.SouthWest)]
        [TestCase(WindDirection.East, WindDirection.West)]
        [TestCase(WindDirection.SouthEast, WindDirection.NorthWest)]
        [TestCase(WindDirection.South, WindDirection.North)]
        [Test]
        public void GetAvgWeatherData_CalculateAverageDirectionWithCrosswindAndDifferentSpeed_DirectionWhereMoreSpeed(
            WindDirection direction1, WindDirection direction2)
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = direction1, WindSpeed = 10 }, 
                new WeatherDataDto { WindDirection = direction2, WindSpeed = 11 }
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindDirection, direction2);
        }

        [Test]
        public void GetAvgWeatherData_CalculateAverageSpeedWithCalm_AverageSpeedHowArithmeticAverage()
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = null, WindSpeed = 0 }, 
                new WeatherDataDto { WindDirection = WindDirection.East, WindSpeed = 20 },
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindSpeed, 10);
        }

        [TestCase(WindDirection.North, WindDirection.South, 10, 11)]
        [TestCase(WindDirection.NorthEast, WindDirection.SouthWest, 10, 16)]
        [TestCase(WindDirection.East, WindDirection.West, 2, 1)]
        [TestCase(WindDirection.SouthEast, WindDirection.NorthWest, 17, 8)]
        [TestCase(WindDirection.South, WindDirection.North, 5, 4)]
        [Test]
        public void GetAvgWeatherData_CalculateAverageSpeedWithCrosswindAndDifferentSpeed_AverageSpeed(
            WindDirection direction1, WindDirection direction2, double speed1, double speed2)
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = direction1, WindSpeed = speed1 }, 
                new WeatherDataDto { WindDirection = direction2, WindSpeed = speed2 }
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindSpeed, Math.Round((speed1 > speed2 ? speed1 - speed2 : speed2 - speed1) / 2, MidpointRounding.AwayFromZero));
        }

        [Test]
        public void GetAvgWeatherData_CalculateAverageSpeedWithCrosswindWhereThreeItems_AverageSpeed()
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = WindDirection.South, WindSpeed = 20 }, 
                new WeatherDataDto { WindDirection = WindDirection.North, WindSpeed = 21 },
                new WeatherDataDto { WindDirection = WindDirection.North, WindSpeed = 2 }
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindSpeed, 1);
        }

        [Test]
        public void GetAvgWeatherData_CalculateCalmWithCrosswind_CalmTrue()
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = WindDirection.North, WindSpeed = 10 }, 
                new WeatherDataDto { WindDirection = WindDirection.South, WindSpeed = 10 },
                new WeatherDataDto { WindDirection = WindDirection.East, WindSpeed = 20 }, 
                new WeatherDataDto { WindDirection = WindDirection.West, WindSpeed = 20 }
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindSpeed, 0);
        }

        [Test]
        public void GetAvgWeatherData_CalculateCalmWithCrosswindAndNotCrosswind_CalmFalse()
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = WindDirection.North, WindSpeed = 10 }, 
                new WeatherDataDto { WindDirection = WindDirection.South, WindSpeed = 10 },
                new WeatherDataDto { WindDirection = WindDirection.NorthWest, WindSpeed = 20 }, 
                new WeatherDataDto { WindDirection = WindDirection.SouthEast, WindSpeed = 20 },
                new WeatherDataDto { WindDirection = WindDirection.West, WindSpeed = 5 }
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreNotEqual(averageWeatherData.WindSpeed, 0);
        }

        [Test]
        public void GetAvgWeatherData_CalculateCalmWith0DegreeAnd360Degree_CalmTrue()
        {
            // Arrange
            var weatherData = new[]
            {
                new WeatherDataDto { WindDirection = WindDirection.North, WindSpeed = 10 }, 
                new WeatherDataDto { WindDirection = WindDirection.South, WindSpeed = 10 },
                new WeatherDataDto { WindDirection = WindDirection.North, WindSpeed = 20 }, 
                new WeatherDataDto { WindDirection = WindDirection.South, WindSpeed = 20 },
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindSpeed, 0);
        }
    }
}
