
using HtmlAgilityPack;

using NUnit.Framework;

using Weather.Common.Message.Request;

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
            var data = this.weatherProvider.Fetch(new ProviderRequest { Url = url });

            // Assert
            Assert.NotNull(data);
        }
    }
}
