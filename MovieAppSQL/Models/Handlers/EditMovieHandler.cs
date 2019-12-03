using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieAppSQL.Models.DataAcessLayers;

namespace MovieAppSQL.Models
{
    public class EditMovieRequestModel : IRequest<EditMovieResponseModelResult>
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
    public class EditMovieResponseModelResult
    {
       public bool Success { get; set; }
    }
    internal class EditMovieHandler : IRequestHandler<EditMovieRequestModel, EditMovieResponseModelResult>
    {
        private readonly IMovieDataAccessLayer _movieDataAccessLayer;
        private readonly IMapper _mapper;
        public EditMovieHandler(IMovieDataAccessLayer movieDataAccessLayer,IMapper mapper) 
        {
            _movieDataAccessLayer = movieDataAccessLayer;
            _mapper = mapper;
        }
        public async Task<EditMovieResponseModelResult> Handle(EditMovieRequestModel request, CancellationToken cancellationToken)
        {
            _movieDataAccessLayer.Update(_mapper.Map<Movie>(request));
            return new EditMovieResponseModelResult() { Success = true};
        }
    }
}