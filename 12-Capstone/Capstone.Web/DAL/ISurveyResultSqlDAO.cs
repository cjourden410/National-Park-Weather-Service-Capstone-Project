using Capstone.Web.Models;
using System.Collections.Generic;

namespace Capstone.Web.DAL
{
    public interface ISurveyResultSqlDAO
    {
        IList<Survey> GetAllSurveyResults();
        bool SaveNewSurvey(Survey survey);
    }
}