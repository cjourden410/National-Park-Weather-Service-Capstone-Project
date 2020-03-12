using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Capstone.Web.Models
{
    public class ParkSearch
    {
        public Park park { get; set; }

        public IList<Weather> WeatherList { get; set; }

        public string GetAdvisory(Weather w)
        {
            return ($"{ conAdvisory[w.Forecast]} { tempAdvisory(w.HighTemp, w.LowTemp)}");
        }
        // Gives advisory based on weather type
        public Dictionary<string, string> conAdvisory = new Dictionary<string, string>()
        {
            { "snow", "Be sure to pack snowshoes!" },
            { "rain", "Be sure to pack rain gear and wear waterproof shoes!" },
            { "thunderstorms", "DANGER Seek shelter and avoid hiking on exposed ridges! DANGER" },
            { "sunny", "Be sure to pack sun block!" },
            { "partly cloudy", "Have an excellent day in this beautiful weather!" },
        };

        // Gives advisory based on temperature
        public string tempAdvisory(int high, int low)
        {
            if (high > 75)
            {
                return "Bring an extra gallon of water.";
            }
            else if ((high - low) > 20)
            {
                return "Wear breathable layers.";
            }
            else if (low < 20)
            {
                return "Exposure to frigid temperatures is dangerous!";
            }
            else
            {
                return "";
            }
        }
        public int tempConversion(int temp)
        {
            int celcius = Convert.ToInt32((temp - 32) / 1.8);
            return celcius;
        }

        // User toggle value for displaying F or C
        public string TempChoice { get; set; } = "F";
    }
}