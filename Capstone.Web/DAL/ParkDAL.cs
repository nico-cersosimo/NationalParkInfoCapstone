using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ParkDAL : IParkDAL
    {
        private string ConnString;


        public ParkDAL(string connString)
        {
            ConnString = connString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> output = new List<Park>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = $"SELECT * FROM park";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        Park park = new Park();
                        park.Name = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Description = Convert.ToString(reader["parkDescription"]);
                        park.ParkCode = Convert.ToString(reader["parkCode"]);

                        output.Add(park);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return output;
        }

        public Park GetPark(string parkCode)
        {
            Park output = new Park();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = $"SELECT * FROM park where parkCode like @parkCode";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        Park park = new Park();
                        park.Name = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Description = Convert.ToString(reader["parkDescription"]);
                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.MilesOfTrail = Convert.ToDecimal(reader["milesOfTrail"]);
                        park.Elevation = Convert.ToInt32(reader["elevationInFeet"]);
                        park.NumOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.Visitors = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.Quote = Convert.ToString(reader["inspirationalQuote"]);
                        park.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.EntryFee = Convert.ToDouble(reader["entryFee"]);
                        park.NumOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

                        output = park;
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
