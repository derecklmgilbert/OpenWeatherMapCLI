using Newtonsoft.Json;
using RestSharp;

namespace OpenWeatherMapCLI.OpenWeatherMap
{
    internal class OpenWeatherMapHelper
    {
        private IRestClient _restClient;
        private const string _apiKey = "";

        public OpenWeatherMapHelper()
        {
            _restClient = new RestClient("http://api.openweathermap.org/geo/1.0");
        }

        public Coordinates GetCoordinatesFromZip(string zipCode)
        {
            var request = new RestRequest($"zip?zip={zipCode}&appid={_apiKey}", Method.Get);
            var response = _restClient.Execute(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<Coordinates>(response.Content);
            }
            else
            {
                throw new Exception("Failed to get coordinates from zip code");
            }
        }

        public Coordinates GetCoordinatesFromCityState(string cityState)
        {
            var request = new RestRequest($"direct?q={cityState}&limit=1&appid={_apiKey}", Method.Get);
            var response = _restClient.Execute(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<List<Coordinates>>(response.Content).First();
            }
            else
            {
                throw new Exception("Failed to get coordinates from city and state");
            }
        }
    }

    public class Coordinates()
    {
        [JsonProperty("lon")]
        public string Longitude { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        public override string ToString()
        {
            return $"Longitude: {Longitude}, Latitude: {Latitude}";
        }
    }
}