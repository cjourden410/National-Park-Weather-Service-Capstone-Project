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

        public SurveyController(ISurveyResultSqlDAO surveyResultDAO)
        {
            this.surveyResultDAO = surveyResultDAO;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return View(survey);
            }

            surveyResultDAO.SaveNewSurvey(survey);
            TempData["Success"] = "Your review has been saved!";
            return RedirectToAction("FavParks");
        }

        public IActionResult FavParks()
        {
            IList<Survey> parks = surveyResultDAO.GetAllSurveyResults();
            return View(parks);
        }
    }
}