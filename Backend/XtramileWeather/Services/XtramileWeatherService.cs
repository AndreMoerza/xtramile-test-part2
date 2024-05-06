using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using XtramileWeather.Model;
using Microsoft.Extensions.Logging;
using XtramileWeather.Controllers;
using System.Threading.Tasks;
using XtramileWeather.Helper;
using Microsoft.Extensions.Configuration;

namespace XtramileWeather.Services
{
    public class XtramileWeatherService : IXtramileWeatherService
    {
        private readonly ILogger<XtramileWeatherService> _logger;
        private readonly IConfiguration _config;


        public XtramileWeatherService(ILogger<XtramileWeatherService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        public async Task<WeatherResponse> GetWeather(string city)
        {
            string apiKey = _config["AppSettings:ApiKey"];
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={city}&APPID={apiKey}"),
                    };

                    using (var response = await httpClient.SendAsync(request))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(apiResponse);

                        weatherResponse.Time = DateTime.UtcNow.AddSeconds(weatherResponse.Timezone);
                        weatherResponse.TemperatureConverter();
                        return weatherResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new WeatherResponse() { ErrorMessage = ex.Message };
            }
        }
    }
}
