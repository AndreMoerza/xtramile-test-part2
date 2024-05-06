using System;
using XtramileWeather.Model;

namespace XtramileWeather.Helper
{
    public static class TemperatureHelper
    {
        public static void TemperatureConverter(this WeatherResponse response)
        {
            if(response.Main != null)
            {
                response.Main.TempC = Math.Round(response.Main.Temp - 273.15, 2);
                response.Main.TempF = Math.Round((response.Main.TempC * 1.8) + 32, 2);
                response.Main.DewPoint = Math.Round(response.Main.TempC - ((100 - response.Main.Humidity) / 5), 2);
            }
        }
    }
}
