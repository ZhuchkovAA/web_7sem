using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Zhuchkov_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private HashSet<string> Summaries=> SharedData.Summaries;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public WeatherForecast Get()
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries.ToList()[rng.Next(Summaries.Count)]
            };
        }

        [HttpGet("week")]
        public IEnumerable<WeatherForecast> GetWeekForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 7).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries.ToList()[rng.Next(Summaries.Count)]
            })
            .ToArray();
        }

        [HttpGet("summaries")]
        [Authorize]
        public IEnumerable<string> GetSummaries()
        {
            return Summaries;
        }

        [HttpGet("choose/{item}")]
        [Authorize(Roles = "admin")]
        public WeatherForecast ChooseWeather(string item)
        {
            Summaries.Add(item);
            return new WeatherForecast { Date = DateTime.Now, Summary = item, TemperatureC = 0 };
        }

        [HttpPost("add/{item}")]
        [Authorize(Roles = "admin")]
        public string AddSummaries(string item)
        {
            Summaries.Add(item);
            return $"Total: {Summaries.Count} items";
        }
    }
}
