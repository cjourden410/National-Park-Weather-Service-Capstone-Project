using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }

        [Required(ErrorMessage = "*")]
        public string ParkCode { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "*")]
        public string State { get; set; }

        [Required(ErrorMessage = "*")]
        public string ActivityLevel { get; set; }

        public string ParkName { get; set; }

        public int SurveyCount { get; set; }
    }
}
