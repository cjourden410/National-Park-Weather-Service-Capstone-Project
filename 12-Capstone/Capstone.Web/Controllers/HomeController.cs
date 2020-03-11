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
    public class HomeController : Controller
    {
        // add reference to DAO
        private IParkSqlDAO parkDAO;
        public HomeController(IParkSqlDAO parkDAO)
        {
            this.parkDAO = parkDAO;
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

        public IActionResult Detail(string id)
        {
            Park park = parkDAO.GetParkById(id);
            ParkSearch ps = new ParkSearch();

            ps.park = park;
            ps.WeatherList = parkDAO.GetWeatherByPark(id);

            return View(ps);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
