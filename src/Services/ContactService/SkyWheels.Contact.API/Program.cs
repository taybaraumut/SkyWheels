using SkyWheels.Contact.API.Extensions.ServiceExtensions;
using SkyWheels.Contact.API.Extensions.MiddlewareExtensions;


var builder = WebApplication.CreateBuilder(args);

// ServiceExtensions
builder.AddCustomServices();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Middleware Extensions
app.UseMiddleware();

app.MapControllers();

app.Run();
