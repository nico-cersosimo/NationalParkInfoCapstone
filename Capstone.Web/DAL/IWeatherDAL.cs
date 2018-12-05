using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public interface IWeatherDAL
    {
        List<Weather> GetWeather(string parkCode);
        List<Weather> GetWeatherCelsius(string parkCode);
    }
}
