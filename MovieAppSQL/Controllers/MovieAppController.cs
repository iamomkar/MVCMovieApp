using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAppSQL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieAppSQL.Models.DataAcessLayers;
using Microsoft.AspNetCore.Http;

namespace MovieAppSQL.Controllers
{
    public class MovieAppController : Controller
    {
        IDataAccessLayer movieDataAcessLayer;

        public MovieAppController(MovieAppDBContext context)
        {
            movieDataAcessLayer = new MovieDataAcessLayerADO();
        }

        public IActionResult Index()
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                User user = new UserDataAcessLayer().GetUserDetails(HttpContext.Session.GetString("email"));
                ViewData["username"] = user.FirstName;
                ViewData["email"] = user.EmailID;
                return View();
            }
        }

        public IActionResult AddMovie()
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                movieDataAcessLayer.Add(movie);
                return RedirectToAction("ViewAll");
            }
            else
            {
                return View(movie);
            }

        }

        public IActionResult ViewAll()
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            return View(movieDataAcessLayer.Movies());
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            Movie movie = movieDataAcessLayer.GetMovieDetails(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            movieDataAcessLayer.Update(movie);
            return RedirectToAction("ViewAll");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            movieDataAcessLayer.Remove(id);
            return RedirectToAction("ViewAll");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index", "Home");
        }

        public bool checkInvalidSession()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                TempData["SessionError"] = "Invalid Session. Login Again";
                return true;
            }
            else return false;
        }


    }
}