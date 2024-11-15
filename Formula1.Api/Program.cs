using Formula1.Api.Endpoints;
using Formula1.Api.ServiceRegistrations;
using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Infrastructure.Middlewares;
using Formula1.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

#region Services

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetVersion).Assembly));
builder.Services.AddExternalServices();
builder.Services.AddInfrastructureServices(builder.Environment);

#endregion
#region Build

var app = builder.Build();

app.UseCors("AllowAllOrigins");
app.MapOpenApi();

app.UseMiddleware<GlobalHttpRequestMiddleware>();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Formula 1 API Reference")
        .WithDarkMode(false)
        .WithSidebar(true)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.MapAliveEndpoints();
app.MapCircuitsEndpoints();
app.MapConstructorsEndpoints();
app.MapDriversEndpoints();
app.MapGrandPrixEndpoints();
app.MapRacesEndpoints();
app.MapResultsEndpoints();
app.MapSeasonsEndpoints();
app.MapSessionsEndpoints();
app.MapSessionTypesEndpoints();

app.UseHttpsRedirection();

#endregion
#region Run

app.Run();

public partial class Program { } // For NUnit WebApplication integration tests

#endregion
