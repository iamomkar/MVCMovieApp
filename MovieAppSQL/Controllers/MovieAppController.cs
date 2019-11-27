using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAppSQL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MovieAppSQL.Controllers
{
    public class MovieAppController : Controller
    {
        //MovieDataAcessLayer movieDataAcessLayer = new MovieDataAcessLayer();
        MovieDataAcessLayer2 movieDataAcessLayer;


        public MovieAppController(MovieAppDBContext context)
        {
            movieDataAcessLayer = new MovieDataAcessLayer2(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
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
            return View(movieDataAcessLayer.Movies());
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Movie movie = movieDataAcessLayer.GetMovieDetails(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
             movieDataAcessLayer.Update(movie);
            return RedirectToAction("ViewAll");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            movieDataAcessLayer.Remove(id);
            return RedirectToAction("ViewAll");
        }


    }
}