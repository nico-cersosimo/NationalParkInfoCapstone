using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public interface ISurveyDAL
    {
        void AddSurvey(Survey surveyModel);
        List<Survey> GetAllSurveyResults();
    }
}
