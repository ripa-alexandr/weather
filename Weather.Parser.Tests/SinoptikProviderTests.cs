
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
    public class SinoptikProviderTests
    {
        private IWeatherProvider weatherProvider;

        [TestFixtureSetUp]
        public void Initialize()
        {
            this.weatherProvider = new SinoptikProvider(new HtmlWeb());
        }

        [Test]
        public void Pars_ParseOneUrl_ShouldParseAll()
        {
            // Arrange
            var url = "http://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D1%83%D0%B0%D0%B9%D1%82%D1%85%D0%BE%D1%80%D1%81-106180550";
            
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
                .Where(i => i.Attribute("name").Value == "Sinoptik")
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
