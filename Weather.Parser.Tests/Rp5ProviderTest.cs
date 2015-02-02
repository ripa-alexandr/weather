
using HtmlAgilityPack;

using NUnit.Framework;

using Weather.Common.Message.Request;

namespace Weather.Parser.Tests
{
    public class Rp5ProviderTest
    {
        private IWeatherProvider weatherProvider;

        [TestFixtureSetUp]
        public void Initialize()
        {
            this.weatherProvider = new Rp5Provider(new HtmlWeb());
        }

        [Test]
        public void Pars_ParsePageWithWeather_ShouldParseAllField()
        {
            // Arrange
            var url = "http://rp5.ua/%D0%9F%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0_%D0%B2_%D0%A7%D1%83%D0%B3%D1%83%D0%B5%D0%B2%D0%B5";

            // Act
            var data = this.weatherProvider.Fetch(new ProviderRequest { Url = url });

            // Assert
            Assert.NotNull(data);
        }
    }
}
