using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Module.Shared.Response;

namespace Module.Infrastructure.Filters
{
    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;
        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!_roles.Any(role => user.IsInRole(role)))
            {
                context.Result = new UnauthorizedObjectResult(new ApiResponse<string>(
                    Success: false,
                    Message: "Forbidden",
                    Data: null
                ));
                return;
            }
        }
    }
}
