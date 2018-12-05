using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class WeatherDAL : IWeatherDAL
    {
        private string ConnString;


        public WeatherDAL(string connString)
        {
            ConnString = connString;
        }

        public List<Weather> GetWeather(string parkCode)
        {
            List<Weather> output = new List<Weather>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = $"SELECT * FROM weather where parkCode like @parkCode";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        Weather weather = new Weather();

                        weather.ParkCode = Convert.ToString(reader["parkCode"]);
                        weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.Low = Convert.ToInt32(reader["low"]);
                        weather.High = Convert.ToInt32(reader["high"]);
                        weather.Forecast = Convert.ToString(reader["forecast"]);

                        output.Add(weather);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }

        public List<Weather> GetWeatherCelsius(string parkCode)
        {
            List<Weather> output = new List<Weather>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = $"SELECT * FROM weather where parkCode like @parkCode";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        Weather weather = new Weather();

                        weather.ParkCode = Convert.ToString(reader["parkCode"]);
                        weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.Low = (Convert.ToInt32(reader["low"]) - 32) * 5 / 9;
                        weather.High = (Convert.ToInt32(reader["high"]) - 32) * 5 / 9;
                        weather.Forecast = Convert.ToString(reader["forecast"]);

                        output.Add(weather);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }
    }
}
