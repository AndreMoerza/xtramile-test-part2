using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XtramileWeather.Helper;
using XtramileWeather.Model;

namespace XtramileWeatherTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TemperatureConverterTest()
        {
            var mockData = GetMockData();
            mockData.TemperatureConverter();

            Assert.AreEqual(mockData.Main.TempC, Math.Round(mockData.Main.Temp - 273.15, 2));
            Assert.AreEqual(mockData.Main.TempF, Math.Round((mockData.Main.TempC * 1.8) + 32, 2));
        }



        private WeatherResponse GetMockData()
        {
            var mockData = new WeatherResponse();

            mockData.Main = new Main();
            mockData.Main.Temp = 300.23;

            return mockData;
        }
    }
}
