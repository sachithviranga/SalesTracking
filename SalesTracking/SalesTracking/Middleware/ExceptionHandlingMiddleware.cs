using Microsoft.AspNetCore.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using System;

namespace SalesTracking.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidCredentialException ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status401Unauthorized, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, bool isLogin = false)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new
            {
                StatusCode = statusCode,
                exception.Message,
                Login = isLogin
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
