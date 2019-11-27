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
        //MovieDataAcessLayerADO movieDataAcessLayer = new MovieDataAcessLayerADO();
        MovieDataAcessLayerEF movieDataAcessLayer;


        public MovieAppController(MovieAppDBContext context)
        {
            movieDataAcessLayer = new MovieDataAcessLayerEF(context);
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