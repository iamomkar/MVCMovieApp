using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieAppSQL.Models.DataAcessLayers;

namespace MovieAppSQL.Models
{
    public class AddMovieRequestModel : IRequest<AddMovieResponseModelResult>
    {
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
    public class AddMovieResponseModelResult
    {
       public bool Success { get; set; }
    }
    internal class AddMovieHandler : IRequestHandler<AddMovieRequestModel, AddMovieResponseModelResult>
    {
        private readonly IMovieDataAccessLayer _movieDataAccessLayer;
        private readonly IMapper _mapper;
        public AddMovieHandler(IMovieDataAccessLayer movieDataAccessLayer,IMapper mapper) 
        {
            _movieDataAccessLayer = movieDataAccessLayer;
            _mapper = mapper;
        }
        public async Task<AddMovieResponseModelResult> Handle(AddMovieRequestModel request, CancellationToken cancellationToken)
        {
            _movieDataAccessLayer.Add(_mapper.Map<Movie>(request));
            return new AddMovieResponseModelResult() { Success = true};
        }
    }
}