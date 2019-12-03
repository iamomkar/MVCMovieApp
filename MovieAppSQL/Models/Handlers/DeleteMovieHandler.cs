using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieAppSQL.Models.DataAcessLayers;

namespace MovieAppSQL.Models
{
    public class DeleteMovieRequestModel : IRequest<DeleteMovieResponseModelResult>
    {
        [Key]
        public int? MovieId { get; set; }

    }
    public class DeleteMovieResponseModelResult
    {
        public bool success { get; set; }
    }
    internal class DeleteMovieHandler : IRequestHandler<DeleteMovieRequestModel, DeleteMovieResponseModelResult>
    {
        private readonly IMovieDataAccessLayer _movieDataAccessLayer;
        public DeleteMovieHandler(IMovieDataAccessLayer movieDataAccessLayer) 
        {
            _movieDataAccessLayer = movieDataAccessLayer;
        }
        public async Task<DeleteMovieResponseModelResult> Handle(DeleteMovieRequestModel request, CancellationToken cancellationToken)
        {
            _movieDataAccessLayer.Remove(request.MovieId);
            return new DeleteMovieResponseModelResult { success = true};
        }
    }
}