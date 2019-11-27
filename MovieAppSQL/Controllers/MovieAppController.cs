using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAppSQL.Models;

namespace MovieAppSQL.Controllers
{
    public class MovieAppController : Controller
    {
        MovieDataAcessLayer movieDataAcessLayer = new MovieDataAcessLayer();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie movie)
        {
            movieDataAcessLayer.AddMovie(movie);
            return RedirectToAction("ViewAll", "MovieApp");
;        }

        public IActionResult ViewAll()
        {
            List<Movie> movies = new List<Movie>();
            movies = movieDataAcessLayer.GetAllMovies();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Movie movie = movieDataAcessLayer.GetMovieDetails(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            movieDataAcessLayer.UpdateMovieDetails(movie);
            return RedirectToAction("ViewAll");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            movieDataAcessLayer.DeleteMovie(id);
            return RedirectToAction("ViewAll");
        }


    }
}