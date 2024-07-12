using AuthOrchestrator.Redis;
//using Tasker.Repositories.Auth.Models;
//using Tasker.Services;
using AuthOrchestrator.Auth;

namespace Tasker.Middlewares
{
    //public interface IUserSessionManager
    //{
    //    User GetCurrentUser();
    //    void SetCurrentUser(User user);
    //}


    public class UserSessionMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly IAuthService _authService;

        public UserSessionMiddleware(RequestDelegate next)
        {
            _next = next;
            //_authService = authService;
        }

        //public async Task InvokeAsync(HttpContext context, IUserSessionManager userSessionManager)
        public async Task InvokeAsync(HttpContext context)
        {
            //var _authService = context.RequestServices.GetRequiredService<IAuthService>();
            ////// Assume username is passed in via header, cookie, or some other means
            ////var username = context.Request.Headers["Username"].FirstOrDefault();

            ////if (!string.IsNullOrEmpty(username))
            ////{
            ////    // Retrieve the user data from Redis
            ////    var user = await _authService.GetUser(username);

            ////    if (user != null)
            ////    {
            ////        // Store the user data in a scoped service for access throughout the request
            ////        userSessionManager.SetCurrentUser(user);
            ////    }
            ////}

            //await _authService.GetUser(context.Request.Headers.Authorization);

            //// Call the next delegate/middleware in the pipeline
            //await _next(context);

            var _authService = context.RequestServices.GetRequiredService<IAuthService>();

            // Assuming the GetUser method expects a token and not a username
            // and that the Authorization header is structured as "Bearer {token}"
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                // Retrieve the user from the token
                var user = await _authService.GetUser(token);

                // If you need to do something with the user object, do it here
                // For example, setting it to some scoped service for later use in the request
            }

            await _next(context);

        }
    }

    //// Extension method for adding the middleware
    //public static class UserSessionMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseUserSession(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<UserSessionMiddleware>();
    //    }
    //}

}
