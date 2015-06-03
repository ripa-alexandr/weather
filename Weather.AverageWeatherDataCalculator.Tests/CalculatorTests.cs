
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

        [TestCase(WindDirectionType.North, WindDirectionType.East, WindDirectionType.NorthEast)]
        [TestCase(WindDirectionType.East, WindDirectionType.South, WindDirectionType.SouthEast)]
        [TestCase(WindDirectionType.South, WindDirectionType.West, WindDirectionType.SouthWest)]
        [TestCase(WindDirectionType.West, WindDirectionType.North, WindDirectionType.NorthWest)]
        [Test]
        public void GetAvgWeatherData_CalculateAverageDirectionEveryFourthWithSameSpeed_AverageDirection(
            WindDirectionType direction1, WindDirectionType direction2, WindDirectionType result)
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

        [TestCase(WindDirectionType.North, WindDirectionType.West, WindDirectionType.NorthWest)]
        [TestCase(WindDirectionType.North, WindDirectionType.East, WindDirectionType.NorthEast)]
        [Test]
        public void GetAvgWeatherData_CalculateAverageDirectionNearZerroWithSameSpeed_AverageDirection(
            WindDirectionType direction1, WindDirectionType direction2, WindDirectionType result)
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
                new WeatherDataDto { WindDirection = WindDirectionType.West, WindSpeed = 0 }, 
                new WeatherDataDto { WindDirection = WindDirectionType.North, WindSpeed = 20 },
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindDirection, WindDirectionType.North);
        }

        [TestCase(WindDirectionType.North, WindDirectionType.South)]
        [TestCase(WindDirectionType.NorthEast, WindDirectionType.SouthWest)]
        [TestCase(WindDirectionType.East, WindDirectionType.West)]
        [TestCase(WindDirectionType.SouthEast, WindDirectionType.NorthWest)]
        [TestCase(WindDirectionType.South, WindDirectionType.North)]
        [Test]
        public void GetAvgWeatherData_CalculateAverageDirectionWithCrosswindAndDifferentSpeed_DirectionWhereMoreSpeed(
            WindDirectionType direction1, WindDirectionType direction2)
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
                new WeatherDataDto { WindDirection = WindDirectionType.North, WindSpeed = 0 }, 
                new WeatherDataDto { WindDirection = WindDirectionType.East, WindSpeed = 20 },
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindSpeed, 10);
        }

        [TestCase(WindDirectionType.North, WindDirectionType.South, 10, 11)]
        [TestCase(WindDirectionType.NorthEast, WindDirectionType.SouthWest, 10, 16)]
        [TestCase(WindDirectionType.East, WindDirectionType.West, 2, 1)]
        [TestCase(WindDirectionType.SouthEast, WindDirectionType.NorthWest, 17, 8)]
        [TestCase(WindDirectionType.South, WindDirectionType.North, 5, 4)]
        [Test]
        public void GetAvgWeatherData_CalculateAverageSpeedWithCrosswindAndDifferentSpeed_AverageSpeed(
            WindDirectionType direction1, WindDirectionType direction2, double speed1, double speed2)
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
                new WeatherDataDto { WindDirection = WindDirectionType.South, WindSpeed = 20 }, 
                new WeatherDataDto { WindDirection = WindDirectionType.North, WindSpeed = 21 },
                new WeatherDataDto { WindDirection = WindDirectionType.North, WindSpeed = 2 }
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
                new WeatherDataDto { WindDirection = WindDirectionType.North, WindSpeed = 10 }, 
                new WeatherDataDto { WindDirection = WindDirectionType.South, WindSpeed = 10 },
                new WeatherDataDto { WindDirection = WindDirectionType.East, WindSpeed = 20 }, 
                new WeatherDataDto { WindDirection = WindDirectionType.West, WindSpeed = 20 }
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
                new WeatherDataDto { WindDirection = WindDirectionType.North, WindSpeed = 10 }, 
                new WeatherDataDto { WindDirection = WindDirectionType.South, WindSpeed = 10 },
                new WeatherDataDto { WindDirection = WindDirectionType.NorthWest, WindSpeed = 20 }, 
                new WeatherDataDto { WindDirection = WindDirectionType.SouthEast, WindSpeed = 20 },
                new WeatherDataDto { WindDirection = WindDirectionType.West, WindSpeed = 5 }
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
                new WeatherDataDto { WindDirection = WindDirectionType.North, WindSpeed = 10 }, 
                new WeatherDataDto { WindDirection = WindDirectionType.South, WindSpeed = 10 },
                new WeatherDataDto { WindDirection = WindDirectionType.North, WindSpeed = 20 }, 
                new WeatherDataDto { WindDirection = WindDirectionType.South, WindSpeed = 20 },
            };

            // Act
            var averageWeatherData = this.calculator.GetAvgWeatherData(weatherData);

            // Assert
            Assert.AreEqual(averageWeatherData.WindSpeed, 0);
        }
    }
}
