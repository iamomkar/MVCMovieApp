using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieAppSQL.Models.DataAcessLayers;

namespace MovieAppSQL.Models
{
    public class GetMovieDetailsRequestModel : IRequest<GetMovieDetailsResponseModelResult>
    {
        [Key]
        public int? MovieId { get; set; }

    }
    public class GetMovieDetailsResponseModelResult
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        public string MovieName { get; set; }

        [Required]
        [Range(1900, 2020)]
        public string ReleaseYear { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }
    }
    internal class GetMovieDetailsHandler : IRequestHandler<GetMovieDetailsRequestModel, GetMovieDetailsResponseModelResult>
    {
        private readonly IMovieDataAccessLayer _movieDataAccessLayer;
        private readonly IMapper _mapper;
        public GetMovieDetailsHandler(IMovieDataAccessLayer movieDataAccessLayer,IMapper mapper) 
        {
            _movieDataAccessLayer = movieDataAccessLayer;
            _mapper = mapper;
        }
        public async Task<GetMovieDetailsResponseModelResult> Handle(GetMovieDetailsRequestModel request, CancellationToken cancellationToken)
        {
            var movie = _mapper.Map<GetMovieDetailsResponseModelResult>(_movieDataAccessLayer.GetMovieDetails(request.MovieId));
            return movie;
        }
    }
}