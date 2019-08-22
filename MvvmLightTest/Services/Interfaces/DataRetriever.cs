using MvvmLightTest.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightTest.Services.Interfaces
{
    public class DataRetriever : IDataRetriever
    {
        private string url;
        private string appId;

        public DataRetriever()
        {
            url = System.Configuration.ConfigurationManager.AppSettings["url"];
            appId = System.Configuration.ConfigurationManager.AppSettings["appId"];

        }

        public CurrentWeather GetWeatherInformation(string city)
        {

            try
            {
                string path = ConstructUrl(city);
                var result = new HttpClient().GetStringAsync(path).Result;
                CurrentWeather data = JsonConvert.DeserializeObject<CurrentWeather>(result);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string ConstructUrl(string city)
        {
            return String.Concat(url, city, appId);
        }
    }
}
