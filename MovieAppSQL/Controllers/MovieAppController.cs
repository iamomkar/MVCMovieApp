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
using MediatR;
using AutoMapper;

namespace MovieAppSQL.Controllers
{
    public class MovieAppController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MovieAppController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var user = _mediator.Send(new GetUserDetailsRequestModel { EmailID = HttpContext.Session.GetString("email")});
                ViewData["username"] = user.Result.FirstName;
                ViewData["email"] = user.Result.EmailID;
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
        public IActionResult AddMovie(AddMovieRequestModel movie)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _mediator.Send(movie);
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
            return View(_mediator.Send(new GetAllMoviesRequestModel()).Result.Movies);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            var movie = _mediator.Send(new GetMovieDetailsRequestModel { MovieId = id });
            return View(_mapper.Map<EditMovieRequestModel>(movie.Result));
        }

        [HttpPost]
        public IActionResult Edit(EditMovieRequestModel movie)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            _mediator.Send(movie);
            return RedirectToAction("ViewAll");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            _mediator.Send(new DeleteMovieRequestModel { MovieId = id });
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