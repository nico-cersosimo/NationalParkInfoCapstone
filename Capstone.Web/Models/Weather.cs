using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public int LowCelsius { get; set; }
        public int HighCelsius { get; set; }
        public string Forecast { get; set; }
        public string TempType { get; set; }

        public static List<SelectListItem> TempTypes = new List<SelectListItem>()
        {
            new SelectListItem {Value = "Fahrenheit", Text="Fahrenheit"},
            new SelectListItem {Value="Celsius", Text="Celsius" },
        };
    }
}
