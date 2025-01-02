using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SkyWheels.Contact.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
                var problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = $"{(int)HttpStatusCode.InternalServerError} Server error",
                    Detail = ex.Message
                };

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
