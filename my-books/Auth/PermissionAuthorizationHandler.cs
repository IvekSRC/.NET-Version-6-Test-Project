using Microsoft.AspNetCore.Authorization;

namespace my_books.Auth
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler() { }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
                return Task.CompletedTask;
            var permissionss = context.User.Claims
                .Where(x => x.Type == CustomClaimTypes.Permission &&
                            x.Value == requirement.Permission);

            if (permissionss.Any())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
