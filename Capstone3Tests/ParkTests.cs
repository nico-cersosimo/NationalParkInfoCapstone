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
    public class ParkTests
    {
        private TransactionScope tran;
        private string connString = @"Data Source =.\SQLEXPRESS;Initial Catalog=NPGeek; Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("insert into park values ('NNP','Nico National Park','Pennsylvania','1000000','20000','3000','1','Tropical Paradise','1999','1','This is my park... not yours','Nico Cersosimo','Only Nico can camp in this park, it is a beautiful perfect paradise designed specifically for him and anyone else will get a disease and die if they try to come anywhere near it.','0','1000');", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllParksTest()
        {
            IParkDAL parkDal = new ParkDAL(connString);

            List<Park> parks = parkDal.GetAllParks();

            Assert.IsNotNull(parks);

            List<string> names = new List<string>();
            foreach (Park park in parks)
            {
                names.Add(park.Name);
            }
            CollectionAssert.Contains(names, "Nico National Park");
        }

        [TestMethod]
        public void GetParkTest()
        {
            IParkDAL parkDal = new ParkDAL(connString);

            Park park = parkDal.GetPark("NNP");

            Assert.IsNotNull(park);

            string parkCode = park.ParkCode;
            string name = park.Name;
            string state = park.State;
            int acreage = park.Acreage;
            decimal milesOfTrail = park.MilesOfTrail;
            int elevation = park.Elevation;
            int numOfCampsites = park.NumOfCampsites;
            string climate = park.Climate;
            int yearFounded = park.YearFounded;
            int visitors = park.Visitors;
            string quote = park.Quote;
            string quoteSource = park.QuoteSource;
            string description = park.Description;
            double entryFee = park.EntryFee;
            int numOfAnimalSpecies = park.NumOfAnimalSpecies;

            Assert.AreEqual("NNP", parkCode);
            Assert.AreEqual("Nico National Park", name);
            Assert.AreEqual("Pennsylvania", state);
            Assert.AreEqual(1000000, acreage);
            Assert.AreEqual(3000, milesOfTrail);
            Assert.AreEqual(20000, elevation);
            Assert.AreEqual(1, numOfCampsites);
            Assert.AreEqual("Tropical Paradise", climate);
            Assert.AreEqual(1999, yearFounded);
            Assert.AreEqual(1, visitors);
            Assert.AreEqual("This is my park... not yours", quote);
            Assert.AreEqual("Nico Cersosimo", quoteSource);
            Assert.AreEqual("Only Nico can camp in this park, it is a beautiful perfect paradise designed specifically for him and anyone else will get a disease and die if they try to come anywhere near it.", description);
            Assert.AreEqual(0, entryFee);
            Assert.AreEqual(1000, numOfAnimalSpecies);
        }
    }
}
