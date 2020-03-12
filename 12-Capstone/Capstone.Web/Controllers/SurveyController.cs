using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyResultSqlDAO surveyResultDAO;

        private IParkSqlDAO parkSqlDAO;

        public SurveyController(ISurveyResultSqlDAO surveyResultDAO, IParkSqlDAO parkSqlDAO)
        {
            this.surveyResultDAO = surveyResultDAO;
            this.parkSqlDAO = parkSqlDAO;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SurveySearch surveysearch)
        {
            if (!ModelState.IsValid)
            {
                return View(surveysearch);
            }

            IList<Park> parks = parkSqlDAO.GetAllParks();
            surveyResultDAO.SaveNewSurvey(surveysearch.survey);
            TempData["Success"] = "Your review has been saved!";
            return RedirectToAction("FavParks");
        }

        public IActionResult FavParks()
        {
            IList<string> parks = surveyResultDAO.GetAllSurveyResults();
            return View(parks);
        }
    }
}