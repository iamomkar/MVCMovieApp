using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MovieAppSQL.Models
{
    public class RequestModel : IRequest<ResponseModelResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
    public class ResponseModelResult 
    {
       public bool Success { get; set; }
    }
    internal class LoginHandler : IRequestHandler<RequestModel, ResponseModelResult>
    {
        IUserDataAcessLayer _userDataAccessLayer;
        public LoginHandler(IUserDataAcessLayer userDataAccessLayer) 
        {
            _userDataAccessLayer = userDataAccessLayer;
        }
        public async Task<ResponseModelResult> Handle(RequestModel request, CancellationToken cancellationToken)
        {
            return new ResponseModelResult() { Success = _userDataAccessLayer.CheckLogin(request.Email, request.Password)};
        }
    }
}