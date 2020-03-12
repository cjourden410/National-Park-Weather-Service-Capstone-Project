using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveyResultSqlDAO : ISurveyResultSqlDAO
    {
        private readonly string connectionString;

        public SurveyResultSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Get list of all survey results
        /// </summary>
        /// <returns></returns>
        public IList<string> GetAllSurveyResults()
        {
            List<string> output = new List<string>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql =
@"select p.parkName, sr.parkCode, count(sr.parkCode) surveysSubmitted
from park p
join survey_result sr on p.parkCode = sr.parkCode
group by sr.parkCode, p.parkName
order by surveysSubmitted DESC, p.parkName ASC";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the command
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Loop through each row
                    while (rdr.Read())
                    {
                        Survey survey = new Survey();

                        survey.ParkName = Convert.ToString(rdr["parkName"]);
                        survey.SurveyCount = Convert.ToInt32(rdr["surveysSubmitted"]);
                        survey.ParkCode = Convert.ToString(rdr["parkCode"]);

                        output.Add(survey.ParkCode + "," + survey.ParkName + "," + survey.SurveyCount);
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
        /// Save a new summary
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        public bool SaveNewSurvey(Survey survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"insert into survey_result (parkCode, emailAddress, state, activityLevel) values(@parkCode, @emailAddress, @state, @activityLevel); Select @@Identity;";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                return false;
                throw ex;
            }
        }

        private Survey RowToObject(SqlDataReader rdr)
        {
            // Create a survey
            Survey survey = new Survey();
            survey.SurveyId = Convert.ToInt32(rdr["surveyId"]);
            survey.ParkCode = Convert.ToString(rdr["parkCode"]);
            survey.EmailAddress = Convert.ToString(rdr["state"]);
            survey.State = Convert.ToString(rdr["acreage"]);
            survey.ActivityLevel = Convert.ToString(rdr["elevationInFeet"]);
            survey.SurveyCount = Convert.ToInt32(rdr["surveysSubmitted"]);
            survey.ParkName = Convert.ToString(rdr["parkName"]);
            return survey;
        }

        private Survey RowToObjectAllSurveys(SqlDataReader rdr)
        {
            // Create a survey
            Survey survey = new Survey();
            survey.ParkName = Convert.ToString(rdr["parkName"]);
            return survey;
        }
    }
}
