using Microsoft.AspNetCore.Mvc;
using RootServiceNamespace;
using SampleService.Services.Client;

namespace SampleService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly HttpClient _httpClient;
        private IRootServiceClient _rootServiceClient;


        public WeatherForecastController(
        //IHttpClientFactory httpClientFactory,
            IRootServiceClient rootServiceClient,
            ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _rootServiceClient = rootServiceClient;
            //_httpClientFactory = httpClientFactory;
            //_httpClient = _httpClientFactory.CreateClient("RootServiceClient");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<IEnumerable<RootServiceNamespace.WeatherForecast>>> Get()
        {
            _logger.LogInformation("WeatherForecastController >>> START  GetWeatherForecast");
            //RootServiceNamespace.RootServiceClient rootServiceClient =
            //    new RootServiceNamespace.RootServiceClient("http://localhost:5284/", _httpClient);
            //return Ok(await rootServiceClient.GetWeatherForecastAsync());
            var res = await _rootServiceClient.Get();
            _logger.LogInformation("WeatherForecastController >>> END  GetWeatherForecast");
            return Ok(res);
        }
    }
}