using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TE.AuthLib;

namespace Capstone.Web.Controllers
{
    public class AppController : Controller
    {
        protected IAuthProvider authProvider;
        public AppController(IAuthProvider authProvider)
        {
            this.authProvider = authProvider;
        }

        protected bool LoggedIn
        {
            get
            {
                return authProvider.IsLoggedIn;
            }
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (LoggedIn)
            {
                ViewData["CurrentUser"] = authProvider.GetCurrentUser();
            }
        }
    }
}