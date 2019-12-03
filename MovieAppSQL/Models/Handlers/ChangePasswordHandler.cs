using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace MovieAppSQL.Models
{
    public class ChangePasswordRequestModel : IRequest<ChangePasswordResponseModelResult>
    {
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
    public class ChangePasswordResponseModelResult
    {
       public bool Success { get; set; }
    }
    internal class ChangePasswordHandler : IRequestHandler<ChangePasswordRequestModel, ChangePasswordResponseModelResult>
    {
        IUserDataAcessLayer _userDataAccessLayer;
        public ChangePasswordHandler(IUserDataAcessLayer userDataAccessLayer) 
        {
            _userDataAccessLayer = userDataAccessLayer;
        }
        public async Task<ChangePasswordResponseModelResult> Handle(ChangePasswordRequestModel request, CancellationToken cancellationToken)
        {
            return new ChangePasswordResponseModelResult() { Success = _userDataAccessLayer.ChangePassword(request.EmailID,request.Password)};
        }
    }
}