using FluentValidation;
using SkyWheels.Contact.API.Context;
using SkyWheels.Contact.API.Dtos;
using SkyWheels.Contact.API.Middlewares;
using SkyWheels.Contact.API.Services;
using SkyWheels.Contact.API.ValidationRules;

namespace SkyWheels.Contact.API.Extensions.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static WebApplicationBuilder AddCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CreateContactDto>, CreateContactValidator>();
            builder.Services.AddScoped<IValidator<UpdateContactDto>, UpdateContactValidator>();
            builder.Services.AddScoped<IContactService,ContactService>();
            builder.Services.AddSingleton<ContactContext>();
            builder.Services.AddTransient<GlobalErrorHandlingMiddleware>();
            builder.Services.AddTransient<ContactValidationMiddleware>();
            //builder.Services.AddScoped<DataProtector>();
            builder.Services.AddDataProtection();
            return builder;
        }
    }
}
