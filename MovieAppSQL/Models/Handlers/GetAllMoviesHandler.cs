using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieAppSQL.Models.DataAcessLayers;

namespace MovieAppSQL.Models
{
    public class GetAllMoviesRequestModel : IRequest<GetAllMoviesResponseModelResult>
    {

    }
    public class GetAllMoviesResponseModelResult
    {
        public IEnumerable<Movie> Movies;
    }
    internal class GetAllMoviesHandler : IRequestHandler<GetAllMoviesRequestModel, GetAllMoviesResponseModelResult>
    {
        private readonly IMovieDataAccessLayer _movieDataAccessLayer;
        public GetAllMoviesHandler(IMovieDataAccessLayer movieDataAccessLayer, IMapper mapper)
        {
            _movieDataAccessLayer = movieDataAccessLayer;
        }
        public async Task<GetAllMoviesResponseModelResult> Handle(GetAllMoviesRequestModel request, CancellationToken cancellationToken)
        {
            return new GetAllMoviesResponseModelResult { Movies = _movieDataAccessLayer.Movies() };
        }
    }
}