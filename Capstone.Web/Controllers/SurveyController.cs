using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {

         ISurveyDAL surveyDAL = new SurveyDAL(@"Data Source=.\SQLEXPRESS;Initial Catalog=NPGeek;Integrated Security=True");

        public IActionResult Index()
        {
            return View("Survey");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostList(Survey surveyModel)
        {
            surveyDAL.AddSurvey(surveyModel);
            return RedirectToAction("Favorites");
        }

        [HttpGet]
        public ActionResult Favorites()
        {
            List<Survey> listOfSurveys = surveyDAL.GetAllSurveyResults();
            return View(listOfSurveys);
        }
    }
}