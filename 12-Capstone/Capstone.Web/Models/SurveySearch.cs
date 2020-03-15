using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TE.AuthLib;

namespace Capstone.Web.Models
{
    public class SurveySearch
    {
        public Survey survey { get; set; }
        public List<SelectListItem> parkList { get; set; }
        public List<SelectListItem> stateList { get; set; }
        public List<string> activityList { get; set; }
        public User user { get; set; } // TODO: add user here so the email is auto filled in for them on the survey page

    }
}
