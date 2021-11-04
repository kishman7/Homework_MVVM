using My_Classwork_Homework_MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace My_Classwork_Homework_MVVM.ViewModel.Helper
{
    public static class WheatherHelper
    {
        // API - application programming interface
        private const string BASE_URL = "http://dataservice.accuweather.com/";
        private const string API_KEY = "B4zhcEh0Xi2rOT43JBA4jndkiujAmd8P";
        private const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        private const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";

        public static async Task<List<City>> GetCitiesAsync(string query) // отримуємо список міст
        {
            var cities = new List<City>();
            var url = BASE_URL + String.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            using(var client = new HttpClient())
            {
                var responce = await client.GetAsync(url);
                var json = await responce.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditionsAsync(string locationKey) // повертає показники погоди в конкретному місці
        {
            var currentConditionsList = new List<CurrentConditions>();

            var url = BASE_URL + String.Format(CURRENT_CONDITIONS_ENDPOINT, locationKey, API_KEY);
            using (var client = new HttpClient())
            {
                var responce = await client.GetAsync(url);
                var json = await responce.Content.ReadAsStringAsync();
                currentConditionsList = JsonConvert.DeserializeObject<List<CurrentConditions>>(json);
            }
            return currentConditionsList.FirstOrDefault();
        }
    }
}
