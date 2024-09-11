namespace OpenWeatherMapCLI
{
    using OpenWeatherMap;

    internal class Program
    {
        private static void Main(string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(GetCoordinates(item));
            }
        }

        public static string GetCoordinates(string location)
        {
            bool isZipCode = int.TryParse(location, out int zipCode);
            var OpenWeatherMapHelper = new OpenWeatherMapHelper();
            if (isZipCode && location.Length == 5)
            {
                try
                {
                    return OpenWeatherMapHelper.GetCoordinatesFromZip(location).ToString();
                }
                catch
                {
                    return $"No information found for zip code: {location}";
                }
            }
            else
            {
                bool isCityState = location.Contains(',');
                if (isCityState)
                {
                    try
                    {
                        return OpenWeatherMapHelper.GetCoordinatesFromCityState(location).ToString();
                    }
                    catch
                    {
                        return $"No information found for location: {location}";
                    }
                }
                else
                {
                    return "Invalid location";
                }
            }
        }
    }
}