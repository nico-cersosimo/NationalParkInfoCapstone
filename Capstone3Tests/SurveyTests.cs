using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Capstone.Web.Controllers;

namespace Capstone3Tests
{
    [TestClass]
    public class SurveyTests
    {
        private TransactionScope tran;
        private string connString = @"Data Source =.\SQLEXPRESS;Initial Catalog=NPGeek; Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO survey_result VALUES ('GTNP', 'thetestguy@tester.com', 'IL', 'Inactive')", conn);
                SqlCommand cmd2 = new SqlCommand("INSERT INTO survey_result VALUES ('GTNP', 'thetestguy@tester.com', 'IL', 'Active')", conn);
                SqlCommand cmd3 = new SqlCommand("INSERT INTO survey_result VALUES ('CVNP', 'thetestguy@tester.com', 'PA', 'Extremely Active')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllSurveysTest()
        {
            ISurveyDAL surveyDal = new SurveyDAL(connString);

            List<Survey> surveys = surveyDal.GetAllSurveyResults();

            Assert.IsNotNull(surveys);
            Assert.AreEqual(3, surveys.Count);
        }
    }
}
