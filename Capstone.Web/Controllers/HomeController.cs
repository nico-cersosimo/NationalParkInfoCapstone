using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        IParkDAL parkDAL = new ParkDAL(@"Data Source=.\SQLEXPRESS;Initial Catalog=NPGeek;Integrated Security=True");
        IWeatherDAL weatherDAL = new WeatherDAL(@"Data Source=.\SQLEXPRESS;Initial Catalog=NPGeek;Integrated Security=True");

        public IActionResult Index()
        {
            HttpContext.Session.SetString("SessionTemperatureKey", "Fahrenheit");
            List<Park> parksList = parkDAL.GetAllParks();
            return View(parksList);
        }

        public IActionResult Detail(string parkCode)
        {
            Park park = parkDAL.GetPark(parkCode);

            List<Weather> weather = new List<Weather>();
            string TempKey = HttpContext.Session.GetString("SessionTemperatureKey");

            if (TempKey == "Fahrenheit")
            {
                weather = weatherDAL.GetWeather(parkCode);
            }
            else
            {
                weather = weatherDAL.GetWeatherCelsius(parkCode);
            }

            Detail detailObject = new Detail();
            detailObject.park = park;
            detailObject.weather = weather;
            return View(detailObject);
        }

        public IActionResult ChangeSessionTemperature (string parkCode, Detail tempDetail)
        {
            HttpContext.Session.SetString("SessionTemperatureKey", tempDetail.newWeatherMeasurement);
            return RedirectToAction("Detail", "Home", new { parkCode = parkCode });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
