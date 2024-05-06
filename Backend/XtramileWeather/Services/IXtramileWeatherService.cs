using System.Threading.Tasks;
using XtramileWeather.Model;

namespace XtramileWeather.Services
{
    public interface IXtramileWeatherService
    {
        public Task<WeatherResponse> GetWeather(string city);
    }
}
