using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TalkerAPI.Authorization
{
    public class AuthorizeJWTAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!Boolean.Parse((string)context.HttpContext.Items["isJwtValid"]))
            {
                context.Result = new JsonResult(new
                {
                    alert = "InvalidJwtToken"
                })
                {
                    StatusCode = 400
                };
            }
        }
    }
}
