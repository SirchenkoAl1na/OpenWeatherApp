using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Entities;
using WeatherApp.Database;


namespace WeatherApp
{
    class Program
    {

        private static readonly HttpClient client = new HttpClient();
        private static string url = "https://api.openweathermap.org/data/2.5/forecast?appid=1a378d6b4d0969a0d1ffbe734285436b&q=Cherkasy&cnt=5";

        async static Task<WeatherForecast?> GetWeather()
        {
            var responseString = await client.GetStringAsync(url);       
            
            return JsonSerializer.Deserialize<WeatherForecast>(responseString);;
        }

        static void SaveWeather(WeatherForecast weatherForecast)
        {
            var db = new Db();

            db.InsertData(weatherForecast);
        }

        async static Task Main(string[] args)
        {
            WeatherForecast? weatherForecast = await GetWeather();

            if (weatherForecast is null)
            {
                Console.WriteLine($"Can`t get a weather forecast");
                return;
            }

            SaveWeather(weatherForecast);

            Console.WriteLine($"cod: {weatherForecast?.cod}");
            Console.WriteLine($"City: {weatherForecast?.city?.name}");
            Console.WriteLine($"list count: {weatherForecast?.list?.Count}");
            foreach (var weather in weatherForecast?.list)
            {
                Console.WriteLine($"weather temp : {weather?.main?.temp}");
            }
        }
    }
}