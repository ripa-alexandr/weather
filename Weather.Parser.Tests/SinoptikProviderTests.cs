
using HtmlAgilityPack;

using NUnit.Framework;

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
        public void Pars_ParsePageWithWeather_ShouldParseAllField()
        {
            // Arrange
            var url = "http://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D1%83%D0%B0%D0%B9%D1%82%D1%85%D0%BE%D1%80%D1%81-106180550";
            
            // Act
            var data = this.weatherProvider.Fetch(url);

            // Assert
            Assert.NotNull(data);
        }
    }
}
