using Microsoft.AspNetCore.Mvc;
using MovieAppSQL.Models;
using Microsoft.AspNetCore.Http;
using MediatR;
using AutoMapper;
using MovieAppSQL.Infrastructure;

namespace MovieAppSQL.Controllers
{
    public class MovieAppController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MovieAppController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult Index()
        {
            var user = _mediator.Send(new GetUserDetailsRequestModel { EmailID = HttpContext.Session.GetString("email") });
            ViewData["username"] = user.Result.FirstName;
            ViewData["email"] = user.Result.EmailID;
            return View();
        }

        [Authorize]
        public IActionResult AddMovie()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddMovie(AddMovieRequestModel movie)
        {
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


        [Authorize]
        public IActionResult ViewAll()
        {
            return View(_mediator.Send(new GetAllMoviesRequestModel()).Result.Movies);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var movie = _mediator.Send(new GetMovieDetailsRequestModel { MovieId = id });
            return View(_mapper.Map<EditMovieRequestModel>(movie.Result));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(EditMovieRequestModel movie)
        {
            _mediator.Send(movie);
            return RedirectToAction("ViewAll");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            _mediator.Send(new DeleteMovieRequestModel { MovieId = id });
            return RedirectToAction("ViewAll");
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index", "Home");
        }

    }
}