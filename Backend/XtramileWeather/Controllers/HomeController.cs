using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using XtramileWeather.Model;
using XtramileWeather.Services;

namespace XtramileWeather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IXtramileWeatherService _weatherService;

        public HomeController(ILogger<HomeController> logger, IXtramileWeatherService weatherService)
        {
            _weatherService = weatherService;
            _logger = logger;
        }


        [HttpGet("weather-city")]
        public async Task<ActionResult> Index(string city)
        {
            try
            {
                var response = await _weatherService.GetWeather(city);
                if(string.IsNullOrEmpty(response.ErrorMessage))
                {
                    return new OkObjectResult(response);
                }
                else
                {
                    return new BadRequestObjectResult(new { Error = response.ErrorMessage });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(new { Error = ex.Message });
            }
        }
    }
}
