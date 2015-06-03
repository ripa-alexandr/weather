
using System;
using System.Collections.Generic;
using System.Linq;

using Weather.AverageWeatherDataCalculator.Interfaces;
using Weather.Common.Dto;
using Weather.Common.Enums;
using Weather.Common.Exceptions;

namespace Weather.AverageWeatherDataCalculator
{
    public class Calculator : ICalculator
    {
        public WeatherDataAggregateDto GetAvgWeatherData(IEnumerable<WeatherDataDto> data)
        {
            var averageWeatherCharacteristic = new WeatherDataAggregateDto
            {
                Providers = data.Select(i => new ProviderDto { Provider = i.Provider, DateTime = i.DateTime }),
                Cloudy = this.AverageCloudy(data.Select(i => i.Cloudy)),
                Precipitation = this.AveragePrecipitation(data.Select(i => i.Precipitation)),
                StrengthPrecipitation = this.AverageStrengthPrecipitation(data.Select(i => i.StrengthPrecipitation)),
                IsFog = this.AverageBool(data.Select(i => i.IsFog)),
                IsThunderstorm = this.AverageBool(data.Select(i => i.IsThunderstorm)),
                AirTemp = this.AverageNumber(data.Select(at => at.AirTemp)),
                RealFeel = this.AverageNumberNullable(data.Select(i => i.RealFeel)),
                Pressure = this.AverageNumber(data.Select(i => i.Pressure)),
                WindDirection = this.AverageWindDirection(data),
                WindSpeed = Round(this.AverageSpeed(data)),
                Humidity = this.AverageNumber(data.Select(i => i.Humidity)),
                ChancePrecipitation = this.AverageNumberNullable(data.Select(i => i.ChancePrecipitation)),
            };

            return averageWeatherCharacteristic;
        }

        private WindDirection? AverageWindDirection(IEnumerable<WeatherDataDto> data)
        {
            var avgDegree = this.AverageDirection(data);

            if (!avgDegree.HasValue) 
                return null;

            if (avgDegree > 337.5 && avgDegree <= 360 || avgDegree >= 0 && avgDegree <= 22.5)
                return WindDirection.North;

            if (avgDegree > 22.5 && avgDegree <= 67.5)
                return WindDirection.NorthEast;

            if(avgDegree > 67.5 && avgDegree <= 112.5)
                return WindDirection.East;

            if (avgDegree > 112.5 && avgDegree <= 157.5)
                return WindDirection.SouthEast;

            if (avgDegree > 157.5 && avgDegree <= 202.5) 
                return WindDirection.South;

            if (avgDegree > 202.5 && avgDegree <= 247.5)
                return WindDirection.SouthWest;

            if (avgDegree > 247.5 && avgDegree <= 292.5)
                return WindDirection.West;

            if (avgDegree > 292.5 && avgDegree <= 337.5)
                return WindDirection.NorthWest;

            throw new NotImplementedException();
        }

        private double? AverageDirection(IEnumerable<WeatherDataDto> data)
        {
            if (data.Any(i => !i.WindDirection.HasValue && i.WindSpeed > 0 || i.WindDirection.HasValue && i.WindSpeed == 0))
                throw new NotCorrectWindDirectionEndWindSpeedException();

            if (data.All(i => !i.WindDirection.HasValue)) 
                return null;

            double ns = 0;
            double ew = 0;

            foreach (var item in data)
            {
                ns += item.WindSpeed * Math.Cos(this.DegToRad(item.WindDirection));
                ew += item.WindSpeed * Math.Sin(this.DegToRad(item.WindDirection));
            }

            ns = ns / data.Count();
            ew = ew / data.Count();

            var atanDegree = this.RadToDeg(Math.Atan2(ns, ew));

            return this.AtanDegTo360Deg(atanDegree);
        }

        private double AverageSpeed(IEnumerable<WeatherDataDto> data)
        {
            if (data.Any(i => !i.WindDirection.HasValue && i.WindSpeed > 0 || i.WindDirection.HasValue && i.WindSpeed == 0))
                throw new NotCorrectWindDirectionEndWindSpeedException();

            double ns = 0;
            double ew = 0;

            foreach (var item in data)
            {
                ns += item.WindSpeed * Math.Cos(this.DegToRad(item.WindDirection));
                ew += item.WindSpeed * Math.Sin(this.DegToRad(item.WindDirection));
            }

            ns = ns / data.Count();
            ew = ew / data.Count();

            return Math.Sqrt(ew * ew + ns * ns);
        }

        private double RadToDeg(double angle)
        {
            return angle / Math.PI * 180.0;
        }

        private double DegToRad(WindDirection? angle)
        {
            return DegToRad(angle.HasValue ? (int)angle : 0);
        }

        private double DegToRad(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double AtanDegTo360Deg(double atan2Degree)
        {
            var degree360 = 90 - atan2Degree;

            return degree360 < 0 ? degree360 + 360 : degree360;
        }

        private CloudyType AverageCloudy(IEnumerable<CloudyType> cloudy)
        {
            var avgCloudy = cloudy.Average(i => (double)i);
            
            if (avgCloudy >= 0 && avgCloudy <= 12.5)
                return CloudyType.Fair;

            if (avgCloudy > 12.5 && avgCloudy <= 37.5)
                return CloudyType.PartlyCloudy;

            if (avgCloudy > 37.5 && avgCloudy <= 62.5)
                return CloudyType.Cloudy;

            if (avgCloudy > 62.5 && avgCloudy <= 87.5)
                return CloudyType.MainlyCloudy;

            if (avgCloudy > 87.5 && avgCloudy <= 100)
                return CloudyType.Overcast;

            throw new NotImplementedException();
        }

        private Precipitation AveragePrecipitation(IEnumerable<Precipitation> precipitation)
        {
            if (this.AverageBool(precipitation.Select(i => i != Precipitation.None)))
            {
                var avg = precipitation.Where(i => i != Precipitation.None).Average(p => (int)p);

                return (Precipitation)Round(avg);
            }

            return Precipitation.None;
        }

        private StrengthPrecipitation AverageStrengthPrecipitation(IEnumerable<StrengthPrecipitation> strengthPrecipitation)
        {
            if (this.AverageBool(strengthPrecipitation.Select(i => i != StrengthPrecipitation.None)))
            {
                var avg = strengthPrecipitation.Where(i => i != StrengthPrecipitation.None).Average(p => (int)p);

                return (StrengthPrecipitation)Round(avg);
            }

            return StrengthPrecipitation.None;
        }

        private bool AverageBool(IEnumerable<bool> itemBools)
        {
            var avgBool = itemBools.Average(i => Convert.ToDouble(i));

            var isBool = Convert.ToBoolean(Round(avgBool));

            return isBool;
        }

        private double AverageNumber(IEnumerable<double> items)
        {
            var avg = Round(items.Average());

            return avg;
        }

        private double? AverageNumberNullable(IEnumerable<double?> items)
        {
            var avg = items.Where(i => i.HasValue);

            if (avg.Count() != 0) 
                return Round(avg.Average(i => i.Value));
            
            return null;
        }

        private double Round(double arg)
        {
            return Math.Round(arg, MidpointRounding.AwayFromZero);
        }
    }
}
