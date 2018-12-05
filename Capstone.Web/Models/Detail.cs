using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Detail
    {
        public Park park { get; set; } = new Park();
        public List<Weather> weather { get; set; } = new List<Weather>();
        public string newWeatherMeasurement { get; set; }
    }
}
