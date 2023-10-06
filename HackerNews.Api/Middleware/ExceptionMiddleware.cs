using System.Net;
using HackerNews.Api.Models;
using HackerNews.Application.Exceptions;
using Microsoft.Net.Http.Headers;

namespace HackerNews.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
               await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem;

            switch (exception)
            {
                case NotFoundException notFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetails()
                    {
                        Title = notFound.Message,
                        Detail = notFound.InnerException?.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException)
                    };
                    break;
                default:
                    problem = new CustomProblemDetails()
                    {
                        Title = exception.Message,
                        Detail = exception.StackTrace,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError)
                    };
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
