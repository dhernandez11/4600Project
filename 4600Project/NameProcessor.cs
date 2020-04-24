using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _4600Project
{
    public class NameProcessor
    {
        public static async Task<CityName> LoadName(int zipCode)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&units=imperial&appid=459263c5be616b620d0e2ecf7700767d";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    CityName nameResult = await response.Content.ReadAsAsync<CityName>();
                    return nameResult;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
