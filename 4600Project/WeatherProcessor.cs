using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace _4600Project
{
    public class WeatherProcessor
    {
        public static async Task<WeatherModel> LoadWeatherInfo(int zipCode)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&units=imperial&appid=459263c5be616b620d0e2ecf7700767d";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    WeatherMain result = await response.Content.ReadAsAsync<WeatherMain>();
                    return result.Main;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
