using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MovieAppSQL.Infrastructure
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (context.HttpContext.Session.GetString("email") == null)
            {
                context.Result = new RedirectResult("~/Home/InvalidSession");
            }
        }

    }
}