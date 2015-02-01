
using HtmlAgilityPack;

using NUnit.Framework;

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
        public void Pars_ParsePageWithWeather_ShouldParseAllField()
        {
            // Arrange
            var url = "http://www.gismeteo.ua/weather-kharkiv-5053/";
            
            // Act
            var data = this.weatherProvider.Fetch(url);

            // Assert
            Assert.NotNull(data);
        }
    }
}
