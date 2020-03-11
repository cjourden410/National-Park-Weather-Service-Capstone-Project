using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkSearch
    {
        public Park park { get; set; }

        public IList<Weather> WeatherList { get; set; }
    }
}
