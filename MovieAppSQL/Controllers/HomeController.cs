using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieAppSQL.Models;

namespace MovieAppSQL.Controllers
{
    public class HomeController : Controller
    {
        UserDataAcessLayer userDataAcessLayer = new UserDataAcessLayer();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        
        
        [HttpPost]
        public IActionResult Login(User user)
        {
            bool success =  userDataAcessLayer.CheckLogin(user);

            if (success)
            {
                HttpContext.Session.SetString("email", user.EmailID);
                return RedirectToAction("Index", "MovieApp");
            }
            else
            {
                ViewData["Error"] = "Invalid Details";
                 return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                bool success = userDataAcessLayer.AddUser(user);
                if (success)
                {
                    ViewData["EmailID"] = user.EmailID;
                    return View("Login");
                }
                else
                {
                    ViewData["Error"] = "Registraion Failed User Already Exists";
                    return View();
                }
            }

            return View(user);
            
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(User user)
        {
            bool success = userDataAcessLayer.ChangePassword(user.EmailID, user.Password);

            if (success)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            { 
                return View();
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
