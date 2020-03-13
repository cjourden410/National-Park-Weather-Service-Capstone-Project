using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAO : IParkSqlDAO
    {
        private readonly string connectionString;

        public ParkSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Weather> GetWeatherByPark(string id)
        {
            List<Weather> output = new List<Weather>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql =
@"select * from weather where parkCode = @code";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", id);

                    // Execute the command
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Loop through each row
                    while (rdr.Read())
                    {
                        Weather weather = new Weather();
                        weather.ParkCode = Convert.ToString(rdr["parkCode"]);
                        weather.FiveDayForecastValue = Convert.ToInt32(rdr["fiveDayForecastValue"]);
                        weather.LowTemp = Convert.ToInt32(rdr["low"]);
                        weather.HighTemp = Convert.ToInt32(rdr["high"]);
                        weather.Forecast = Convert.ToString(rdr["forecast"]);
                        output.Add(weather);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return output;
        }

        /// <summary>
        /// Get park by id and display all info as well as the 5 day forecast for that specific park
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Park GetParkById(string id)
        {
            Park park = new Park();
            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql =
@"select * from park p
join weather w on p.parkCode = w.parkCode
where p.parkCode = @code";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", id);

                    // Execute the command
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Loop through each row
                    if (rdr.Read())
                    {
                        // Create a park
                        park.ParkCode = Convert.ToString(rdr["parkCode"]);
                        park.ParkName = Convert.ToString(rdr["parkName"]);
                        park.State = Convert.ToString(rdr["state"]);
                        park.Acreage = Convert.ToInt32(rdr["acreage"]);
                        park.ElevationInFeet = Convert.ToInt32(rdr["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToSingle(rdr["milesOfTrail"]);
                        park.NumberOfCampsites = Convert.ToInt32(rdr["numberOfCampsites"]);
                        park.Climate = Convert.ToString(rdr["climate"]);
                        park.YearFounded = Convert.ToInt32(rdr["yearFounded"]);
                        park.AnnualVisitorCount = Convert.ToInt32(rdr["annualVisitorCount"]);
                        park.InspirationalQuote = Convert.ToString(rdr["inspirationalQuote"]);
                        park.InspirationalQuoteSource = Convert.ToString(rdr["inspirationalQuoteSource"]);
                        park.ParkDescription = Convert.ToString(rdr["parkDescription"]);
                        park.EntryFee = Convert.ToInt32(rdr["entryFee"]);
                        park.NumberOfAnimalSpecies = Convert.ToInt32(rdr["numberOfAnimalSpecies"]);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return park;
        }

        /// <summary>
        /// Returns all of the parks
        /// </summary>
        /// <returns></returns>
        public IList<Park> GetAllParks()
        {
            List<Park> output = new List<Park>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql =
@"select * from park";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the command
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Loop through each row
                    while (rdr.Read())
                    {
                        Park park = new Park();
                        park.ParkCode = Convert.ToString(rdr["parkCode"]);
                        park.ParkName = Convert.ToString(rdr["parkName"]);
                        park.State = Convert.ToString(rdr["state"]);
                        park.Acreage = Convert.ToInt32(rdr["acreage"]);
                        park.ElevationInFeet = Convert.ToInt32(rdr["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToSingle(rdr["milesOfTrail"]);
                        park.NumberOfCampsites = Convert.ToInt32(rdr["numberOfCampsites"]);
                        park.Climate = Convert.ToString(rdr["climate"]);
                        park.YearFounded = Convert.ToInt32(rdr["yearFounded"]);
                        park.AnnualVisitorCount = Convert.ToInt32(rdr["annualVisitorCount"]);
                        park.InspirationalQuote = Convert.ToString(rdr["inspirationalQuote"]);
                        park.InspirationalQuoteSource = Convert.ToString(rdr["inspirationalQuoteSource"]);
                        park.ParkDescription = Convert.ToString(rdr["parkDescription"]);
                        park.EntryFee = Convert.ToInt32(rdr["entryFee"]);
                        park.NumberOfAnimalSpecies = Convert.ToInt32(rdr["numberOfAnimalSpecies"]);
                        output.Add(park);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return output;
        }

        public List<SelectListItem> GetParksForSurvey()
        {
            List<SelectListItem> output = new List<SelectListItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "select parkName, parkCode from park";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string parkName = Convert.ToString(reader["parkName"]);
                        string parkCode = Convert.ToString(reader["parkCode"]);
                        output.Add(new SelectListItem() { Text = parkName, Value = parkCode });
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return output;
        }
    }

    // No longer using RowToObject as it conflicts when using System.Web.Mvc
    //private Park RowToObject(SqlDataReader rdr)
    //{
    //    // Create a park
    //    Park park = new Park();
    //    park.ParkCode = Convert.ToString(rdr["parkCode"]);
    //    park.ParkName = Convert.ToString(rdr["parkName"]);
    //    park.State = Convert.ToString(rdr["state"]);
    //    park.Acreage = Convert.ToInt32(rdr["acreage"]);
    //    park.ElevationInFeet = Convert.ToInt32(rdr["elevationInFeet"]);
    //    park.MilesOfTrail = Convert.ToSingle(rdr["milesOfTrail"]);
    //    park.NumberOfCampsites = Convert.ToInt32(rdr["numberOfCampsites"]);
    //    park.Climate = Convert.ToString(rdr["climate"]);
    //    park.YearFounded = Convert.ToInt32(rdr["yearFounded"]);
    //    park.AnnualVisitorCount = Convert.ToInt32(rdr["annualVisitorCount"]);
    //    park.InspirationalQuote = Convert.ToString(rdr["inspirationalQuote"]);
    //    park.InspirationalQuoteSource = Convert.ToString(rdr["inspirationalQuoteSource"]);
    //    park.ParkDescription = Convert.ToString(rdr["parkDescription"]);
    //    park.EntryFee = Convert.ToInt32(rdr["entryFee"]);
    //    park.NumberOfAnimalSpecies = Convert.ToInt32(rdr["numberOfAnimalSpecies"]);
    //    return park;
    //}

    //private Weather RowToObjectWeather(SqlDataReader rdr)
    //{
    //    // Create a park
    //    Weather weather = new Weather();
    //    weather.ParkCode = Convert.ToString(rdr["parkCode"]);
    //    weather.FiveDayForecastValue = Convert.ToInt32(rdr["fiveDayForecastValue"]);
    //    weather.LowTemp = Convert.ToInt32(rdr["low"]);
    //    weather.HighTemp = Convert.ToInt32(rdr["high"]);
    //    weather.Forecast = Convert.ToString(rdr["forecast"]);
    //    return weather;
    //}
}

