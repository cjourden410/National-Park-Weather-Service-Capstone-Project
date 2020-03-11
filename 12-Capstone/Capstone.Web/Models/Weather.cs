using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int LowTemp { get; set; } // TODO: allow user to convert to celcius
        public int HighTemp { get; set; } // TODO: allow user to convert to celcius
        public string Forecast { get; set; }
    }
}
