using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace MovieAppSQL.Models
{
    public class RegisterRequestModel : IRequest<RegisterResponseModelResult>
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }

    }
    public class RegisterResponseModelResult 
    {
       public bool Success { get; set; }
    }
    internal class RegistrationHandler : IRequestHandler<RegisterRequestModel, RegisterResponseModelResult>
    {
        IUserDataAcessLayer _userDataAccessLayer;
        private readonly IMapper _mapper;
        public RegistrationHandler(IUserDataAcessLayer userDataAccessLayer,IMapper mapper) 
        {
            _userDataAccessLayer = userDataAccessLayer;
            _mapper = mapper;
        }
        public async Task<RegisterResponseModelResult> Handle(RegisterRequestModel request, CancellationToken cancellationToken)
        {
            return new RegisterResponseModelResult() { Success = _userDataAccessLayer.AddUser(_mapper.Map<User>(request))};
        }
    }
}