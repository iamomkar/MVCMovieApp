using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieAppSQL.Models;
using MediatR;

namespace MovieAppSQL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {            
            _mediator = mediator;
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
            var result = _mediator.Send(new RequestModel() { Email = user.EmailID,Password = user.Password });
            if (result.Result.Success)
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
        public ActionResult Register(RegisterRequestModel registerRequestModel)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(registerRequestModel);
                if (result.Result.Success)
                {
                    ViewData["EmailID"] = registerRequestModel.EmailID;
                    return View("Login");
                }
                else
                {
                    ViewData["Error"] = "Registraion Failed User Already Exists";
                    return View(registerRequestModel);
                }
            }

            return View();
            
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordRequestModel changePasswordRequestModel)
        {
            var result = _mediator.Send(changePasswordRequestModel);
            if (result.Result.Success)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            { 
                return View();
            }
        }

        public IActionResult InvalidSession()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
