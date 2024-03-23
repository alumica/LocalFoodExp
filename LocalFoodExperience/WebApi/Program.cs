using WebApi.Endpoints;
using WebApi.Extensions;
using WebApi.Mapsters;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container
    builder
        .ConfigureCors()
        .ConfigureServices()
        .ConfigureSwaggerOpenApi()
        .ConfigureMapster();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline
    app.SetupRequestPipline();

    // Configure API endpoints
    app.MapUserEndpoints();
    app.MapDishEndpoints();
    app.MapReviewEndpoints();
    app.MapHostEndpoints();

    app.Run();
}

app.Run();