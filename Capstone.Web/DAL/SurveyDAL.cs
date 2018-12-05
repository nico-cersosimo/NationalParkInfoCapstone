using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class SurveyDAL : ISurveyDAL
    {

        private readonly string connectionString;

        public SurveyDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }


        const string SQL_AddSurvey = @"INSERT INTO survey_result VALUES (@ParkCode, @Email, @State, @ActivityLevel)";
        const string SQL_GetAllSurveyResults = @"SELECT COUNT(*) AS numberOfSurveys, survey_result.parkCode, parkName FROM survey_result join park ON park.parkCode=survey_result.parkCode GROUP BY survey_result.parkCode, parkName ORDER BY numberOfSurveys DESC";

        public void AddSurvey(Survey surveyModel)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_AddSurvey;
                    cmd.Parameters.AddWithValue("@ParkCode", surveyModel.ParkCode);
                    cmd.Parameters.AddWithValue("@Email", surveyModel.Email);
                    cmd.Parameters.AddWithValue("@State", surveyModel.State);
                    cmd.Parameters.AddWithValue("@ActivityLevel", surveyModel.ActivityLevel);

                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Survey> GetAllSurveyResults()
        {
            List<Survey> allSurveys = new List<Survey>();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_GetAllSurveyResults;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Survey tempSurvey = new Survey();
                        tempSurvey.ParkCode = Convert.ToString(reader["parkCode"]);
                        tempSurvey.ParkName = Convert.ToString(reader["parkName"]);
                        tempSurvey.Votes = Convert.ToInt32(reader["numberOfSurveys"]);
                        allSurveys.Add(tempSurvey);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return allSurveys;
        }
    }
}
