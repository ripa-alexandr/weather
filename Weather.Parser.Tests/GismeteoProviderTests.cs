
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using HtmlAgilityPack;

using NUnit.Framework;

using Weather.Common.Dto;

namespace Weather.Parser.Tests
{
    [TestFixture]
    public class GismeteoProviderTests
    {
        private IWeatherProvider weatherProvider;

        [TestFixtureSetUp]
        public void Initialize()
        {
            this.weatherProvider = new GismeteoProvider(new HtmlWeb());
        }

        [Test]
        public void Pars_ParseOneUrl_ShouldParseAll()
        {
            // Arrange
            var url = "http://www.gismeteo.ua/weather-kharkiv-5053/";
            
            // Act
            var data = this.weatherProvider.Fetch(url);

            // Assert
            Assert.NotNull(data);
        }

        [Test]
        public void Pars_ParseAllUrls_ShouldParseAll()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Links.xml");
            var doc = XDocument.Load(filePath);
            var links = doc.Root.Descendants("link")
                .Where(i => i.Attribute("name").Value == "Gismeteo")
                .Select(i => i.Attribute("url").Value);
            var data = new List<WeatherDataDto>();

            // Act
            foreach (var link in links)
            {
                data.AddRange(this.weatherProvider.Fetch(link));
            }

            // Assert
            Assert.NotNull(data);
        }
    }
}
