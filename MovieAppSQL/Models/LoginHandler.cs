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
        public string ResponseText { get; set; }
    }
    internal class LoginHandler : IRequestHandler<RequestModel, ResponseModelResult>
    {
        public LoginHandler() 
        {
        
        }
        public async Task<ResponseModelResult> Handle(RequestModel request, CancellationToken cancellationToken)
        {
            UserDataAcessLayer userDataAcessLayer = new UserDataAcessLayer();
            bool success = userDataAcessLayer.CheckLogin(request.Email, request.Password);
            return new ResponseModelResult() { Success = success, ResponseText = "Login Successfull"};
        }
    }
}