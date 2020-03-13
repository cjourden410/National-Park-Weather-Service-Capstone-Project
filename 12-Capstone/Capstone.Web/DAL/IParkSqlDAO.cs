using Capstone.Web.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.DAL
{
    public interface IParkSqlDAO
    {
        IList<Park> GetAllParks();
        Park GetParkById(string code);
        IList<Weather> GetWeatherByPark(string id);
        List<SelectListItem> GetParksForSurvey();
    }
}