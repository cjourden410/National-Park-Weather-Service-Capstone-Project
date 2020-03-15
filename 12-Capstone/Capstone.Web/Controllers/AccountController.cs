using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using TE.AuthLib;

namespace Capstone.Web.Controllers
{
    public class AccountController : AppController
    {
        public AccountController(IAuthProvider authProvider) : base(authProvider)
        {
        }

        [Authorize]  //Must be logged in to see Account/Index page
        [HttpGet]
        public IActionResult Index()
        {
            var user = authProvider.GetCurrentUser();
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            // Ensure the fields were filled out
            if (ModelState.IsValid)
            {
                // Check that they provided correct credentials
                bool validLogin = authProvider.Login(login.Email, login.Password);
                if (validLogin)
                {
                    // Redirect the user where you want them to go after successful login
                    return RedirectToAction("Index", "Home");
                }
            }
            // Inform Bad username or password. Have a link at the bottom that says "Don't have a National Park Geek Account? Sign Up (hotlink to register page)"
            TempData["Message"] = "Bad username or password.";
            return View(login);
        }

        [Authorize] // Must be logged in to log out
        [HttpGet]
        public IActionResult Logout()
        {
            // Clear user from session
            authProvider.Logout();

            // Redirect the user where you want them to go after logoff
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterNewUser registerNewUser)
        {
            if (!ModelState.IsValid)
            {
                return View(registerNewUser);
            }

            // Register them as a new user (and set default role)
            // When a user registers they need to be given a role. If you don't need anything special
            // just give them "User".
            authProvider.Register(registerNewUser.Email, registerNewUser.Password, role: "User");

            // Redirect the user where you want them to go after registering
            return RedirectToAction("Index", "Account");
        }
    }
}