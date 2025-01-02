using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SkyWheels.Contact.API.Dtos;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SkyWheels.Contact.API.Middlewares
{
    public class ContactValidationMiddleware : IMiddleware
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ContactValidationMiddleware(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                if (context.Request.Path == "/sky-wheels-api/contact/create-contact" ||
                context.Request.Path == "/sky-wheels-api/contact/update-contact")
                {

                    var jsonRequestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

                    var content = Encoding.UTF8.GetBytes(jsonRequestBody!);
                    var requestBodyStream = new MemoryStream();
                    requestBodyStream.Write(content, 0, content.Length);
                    context.Request.Body = requestBodyStream;
                    context.Request.Body.Seek(0, SeekOrigin.Begin);

                    var json = JsonSerializer.Deserialize<CreateContactDto>(jsonRequestBody!);

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var validator = scope.ServiceProvider.GetRequiredService<IValidator<CreateContactDto>>();
                        var validator_two = scope.ServiceProvider.GetRequiredService<IValidator<UpdateContactDto>>();

                        var validationResult = await validator.ValidateAsync(json!);
                        var validationResult_two = await validator.ValidateAsync(json!);

                        if (!validationResult.IsValid || !validationResult_two.IsValid)
                        {
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsJsonAsync(validationResult.Errors.Select(x => x.ErrorMessage));
                            return;
                        }
                    }

                }

                await next(context);
            }
            catch (Exception ex)
            {               
                var problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Internal Server error",
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
