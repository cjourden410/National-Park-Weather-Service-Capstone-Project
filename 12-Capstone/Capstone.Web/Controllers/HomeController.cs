using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TE.AuthLib;
using TE.AuthLib.DAL;

namespace Capstone.Web.Controllers
{
    public class HomeController : AppController
    {
        // add reference to DAO
        private IParkSqlDAO parkDAO;
        private IUserDAO userDAO;
        public HomeController(IParkSqlDAO parkDAO, IUserDAO userDAO, IAuthProvider authProvider) : base(authProvider)
        {
            this.parkDAO = parkDAO;
            this.userDAO = userDAO;
        }

        public IActionResult Index()
        {
            IList<Park> parks = parkDAO.GetAllParks();
            return View(parks);
        }


        //public IActionResult Detail(string id) // Works to get the single park no weather
        //{
        //    Park park = parkDAO.GetParkById(id);
        //    IList<Weather> weather = parkDAO.GetWeatherByPark(id);
        //    return View(park);
        //}
        //[HttpGet]
        //public IActionResult Detail(string id)
        //{
        //    Park park = parkDAO.GetParkById(id);
        //    ParkSearch ps = new ParkSearch();

        //    ps.park = park;
        //    ps.WeatherList = parkDAO.GetWeatherByPark(id);
        //    string tempChoice = HttpContext.Session.GetString("tempChoice");
        //    if (tempChoice == null)
        //    {
        //        tempChoice = "F";
        //        HttpContext.Session.SetString("tempChoice", tempChoice);
        //    }
        //    ps.TempChoice = tempChoice;

        //    return View(ps);
        //}

        [HttpGet]
        public IActionResult Detail(string id, User user)
        {
            Park park = parkDAO.GetParkById(id);
            ParkSearch ps = new ParkSearch();

            ps.park = park;
            ps.WeatherList = parkDAO.GetWeatherByPark(id);

            if (user.Id > 0)
            {
                user.TempPref = HttpContext.Session.GetString("tempChoice") ?? "F";
                ps.TempChoice = user.TempPref;
                //userDAO.UpdateUser(user);
            }
            else
            {
                ps.TempChoice = HttpContext.Session.GetString("tempChoice") ?? "F";
            }

            //ps.TempChoice = HttpContext.Session.GetString("tempChoice") ?? "F";

            return View(ps);
        }

        [HttpPost]
        public IActionResult Detail(string id, ParkSearch ps, User user)
        {
            if (user.Id > 0)
            {
                //user.TempPref = HttpContext.Session.GetString("tempChoice") ?? "F";
                ps.TempChoice = user.TempPref;
                userDAO.UpdateUser(user);
            }
            HttpContext.Session.SetString("tempChoice", ps.TempChoice);
            ps.park = parkDAO.GetParkById(id);
            ps.WeatherList = parkDAO.GetWeatherByPark(id);

            return RedirectToAction("Detail");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
