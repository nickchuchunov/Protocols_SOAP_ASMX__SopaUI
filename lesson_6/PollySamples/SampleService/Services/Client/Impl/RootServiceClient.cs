﻿namespace SampleService.Services.Client.Impl
{
    public class RootServiceClient : IRootServiceClient
    {


        private RootServiceNamespace.RootServiceClient _httpClient;

        public RootServiceClient(HttpClient httpClient)
        {
            _httpClient = new RootServiceNamespace.RootServiceClient("http://localhost:5284/", httpClient);
        }

        public RootServiceNamespace.RootServiceClient Client
        {
            get { return _httpClient; }
        }

        public async Task<ICollection<RootServiceNamespace.WeatherForecast>> Get()
        {
            return await _httpClient.GetWeatherForecastAsync();
        }


    }
}
