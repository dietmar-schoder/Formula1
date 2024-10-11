using Formula1.Api.Endpoints;
using Formula1.Api.Extensions;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

#region Services

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Formula 1 API Reference", Version = "1.0" });
});
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetVersionQueryHandler).Assembly));
builder.Services.AddInfrastructureServices();

#endregion
#region Build

var app = builder.Build();

app.UseSwagger(options =>
{
    options.RouteTemplate = "openapi/{documentName}.json";
});
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Formula 1 API Reference")
        .WithSidebar(false)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.MapAliveEndpoints();
app.MapCircuitsEndpoints();
app.MapConstructorsEndpoints();
app.MapDriversEndpoints();
app.MapGrandPrixEndpoints();
app.MapRacesEndpoints();
app.MapSeasonsEndpoints();
app.MapSessionsEndpoints();
app.MapSessionTypesEndpoints();

app.UseHttpsRedirection();

#endregion
#region Run

app.Run();

public partial class Program { } // For NUnit WebApplication integration tests

#endregion
