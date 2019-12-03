using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace MovieAppSQL.Models
{
    public class GetUserDetailsRequestModel : IRequest<GetUserDetailsResponseModelResult>
    {
        public string EmailID { get; set; }

    }
    public class GetUserDetailsResponseModelResult
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailID { get; set; }
    }
    internal class GetUserDetailsHandler : IRequestHandler<GetUserDetailsRequestModel, GetUserDetailsResponseModelResult>
    {
        IUserDataAcessLayer _userDataAccessLayer;
        private readonly IMapper _mapper;
        public GetUserDetailsHandler(IUserDataAcessLayer userDataAccessLayer,IMapper mapper) 
        {
            _userDataAccessLayer = userDataAccessLayer;
            _mapper = mapper;
        }
        public async Task<GetUserDetailsResponseModelResult> Handle(GetUserDetailsRequestModel request, CancellationToken cancellationToken)
        {
            GetUserDetailsResponseModelResult getUserDetails = _mapper.Map<GetUserDetailsResponseModelResult>(_userDataAccessLayer.GetUserDetails(request.EmailID));

            return getUserDetails;
        }
    }
}