using Capstone.Web.Models;
using System.Collections.Generic;

namespace Capstone.Web.DAL
{
    public interface IParkSqlDAO
    {
        IList<Park> GetAllParks();
        Park GetParkById(string code);
        IList<Weather> GetWeatherByPark(string id);
    }
}