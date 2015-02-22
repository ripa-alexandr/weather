
using System;
using System.Collections.Generic;
using System.Linq;

using Weather.AverageWeatherDescriptionCalculator.Interfaces;
using Weather.Common.Entities;

namespace Weather.AverageWeatherDescriptionCalculator
{
    public class Calculator : ICalculator
    {
        public WeatherDescriptionEntity GetAvgWeatherDescription(IEnumerable<WeatherDescriptionEntity> descriptions)
        {
            var averageWeatherCharacteristic = new WeatherDescriptionEntity
            {
                Cloudy = this.AverageCloudy(descriptions.Select(i => i.Cloudy)),
                Precipitation = this.AverageTypePrecipitation(descriptions.Select(i => i.Precipitation)),
                StrengthPrecipitation = this.AverageStrengthPrecipitation(descriptions.Select(i => i.StrengthPrecipitation)),
                IsFog = this.AverageBool(descriptions.Select(i => i.IsFog)),
                IsThunderstorm = this.AverageBool(descriptions.Select(i => i.IsThunderstorm)),
                AirTemp = this.AverageNumber(descriptions.Select(at => at.AirTemp)),
                RealFeel = this.AverageNumberNullable(descriptions.Select(i => i.RealFeel)),
                Pressure = this.AverageNumber(descriptions.Select(i => i.Pressure)),
                WindDirection = this.AverageWindDirection(descriptions),
                WindSpeed = Math.Round(this.AverageSpeed(descriptions)),
                Humidity = this.AverageNumber(descriptions.Select(i => i.Humidity)),
                ChancePrecipitation = this.AverageNumberNullable(descriptions.Select(i => i.ChancePrecipitation)),
            };

            return averageWeatherCharacteristic;
        }

        private WindDirectionTypeEntity AverageWindDirection(IEnumerable<WeatherDescriptionEntity> descriptions)
        {
            var avgDegree = this.AverageDirection(descriptions);

            if (avgDegree > 337.5 && avgDegree <= 360 || avgDegree >= 0 && avgDegree <= 22.5)
                return WindDirectionTypeEntity.North;

            if (avgDegree > 22.5 && avgDegree <= 67.5)
                return WindDirectionTypeEntity.NorthEast;

            if(avgDegree > 67.5 && avgDegree <= 112.5)
                return WindDirectionTypeEntity.East;

            if (avgDegree > 112.5 && avgDegree <= 157.5)
                return WindDirectionTypeEntity.SouthEast;

            if (avgDegree > 157.5 && avgDegree <= 202.5) 
                return WindDirectionTypeEntity.South;

            if (avgDegree > 202.5 && avgDegree <= 247.5)
                return WindDirectionTypeEntity.SouthWest;

            if (avgDegree > 247.5 && avgDegree <= 292.5)
                return WindDirectionTypeEntity.West;

            if (avgDegree > 292.5 && avgDegree <= 337.5)
                return WindDirectionTypeEntity.NorthWest;

            throw new NotImplementedException();
        }

        private double AverageDirection(IEnumerable<WeatherDescriptionEntity> descriptions)
        {
            double ns = 0;
            double ew = 0;

            foreach (var description in descriptions)
            {
                ns += description.WindSpeed * Math.Cos(this.DegToRad((int)description.WindDirection));
                ew += description.WindSpeed * Math.Sin(this.DegToRad((int)description.WindDirection));
            }

            ns = ns / descriptions.Count();
            ew = ew / descriptions.Count();

            var atanDegree = this.RadToDeg(Math.Atan2(ns, ew));

            return this.AtanDegTo360Deg(atanDegree);
        }

        private double AverageSpeed(IEnumerable<WeatherDescriptionEntity> descriptions)
        {
            double ns = 0;
            double ew = 0;

            foreach (var description in descriptions)
            {
                ns += description.WindSpeed * Math.Cos(this.DegToRad((int)description.WindDirection));
                ew += description.WindSpeed * Math.Sin(this.DegToRad((int)description.WindDirection));
            }

            ns = ns / descriptions.Count();
            ew = ew / descriptions.Count();

            return Math.Sqrt(ew * ew + ns * ns);
        }

        private double RadToDeg(double angle)
        {
            return angle / Math.PI * 180.0;
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

        private CloudyTypeEntity AverageCloudy(IEnumerable<CloudyTypeEntity> cloudy)
        {
            var avgCloudy = cloudy.Average(i => (double)i);
            
            if (avgCloudy >= 0 && avgCloudy <= 12.5)
                return CloudyTypeEntity.Fair;

            if (avgCloudy > 12.5 && avgCloudy <= 37.5)
                return CloudyTypeEntity.PartlyCloudy;

            if (avgCloudy > 37.5 && avgCloudy <= 62.5)
                return CloudyTypeEntity.Cloudy;

            if (avgCloudy > 62.5 && avgCloudy <= 87.5)
                return CloudyTypeEntity.MainlyCloudy;

            if (avgCloudy > 87.5 && avgCloudy <= 100)
                return CloudyTypeEntity.Overcast;

            throw new NotImplementedException();
        }

        private PrecipitationTypeEntity AverageTypePrecipitation(IEnumerable<PrecipitationTypeEntity> precipitation)
        {
            if (this.AverageBool(precipitation.Select(i => i == PrecipitationTypeEntity.None)))
                return PrecipitationTypeEntity.None;

            var avgTypePrecipitation = Math.Round(precipitation.Where(i => i != PrecipitationTypeEntity.None).Average(p => (int)p));

            return (PrecipitationTypeEntity)avgTypePrecipitation;
        }

        private StrengthPrecipitationTypeEntity AverageStrengthPrecipitation(IEnumerable<StrengthPrecipitationTypeEntity> strengthPrecipitation)
        {
            if (this.AverageBool(strengthPrecipitation.Select(i => i == StrengthPrecipitationTypeEntity.None)))
                return StrengthPrecipitationTypeEntity.None;

            var avgStrengthPrecipitation = Math.Round(strengthPrecipitation.Where(i => i != StrengthPrecipitationTypeEntity.None).Average(p => (int)p));

            return (StrengthPrecipitationTypeEntity)avgStrengthPrecipitation;
        }

        private bool AverageBool(IEnumerable<bool> itemBools)
        {
            var avgBool = itemBools.Average(i => Convert.ToDouble(i));

            var isBool = Convert.ToBoolean(Math.Round(avgBool));

            return isBool;
        }

        private double AverageNumber(IEnumerable<double> items)
        {
            var avg = Math.Round(items.Average());

            return avg;
        }

        private double? AverageNumberNullable(IEnumerable<double?> items)
        {
            var avg = items.Where(i => i.HasValue);

            if (avg.Count() != 0) 
                return Math.Round(avg.Average(i => i.Value));
            
            return null;
        }
    }
}
