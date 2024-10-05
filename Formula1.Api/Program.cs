using Formula1.Api.Endpoints;
using Formula1.Api.Extensions;
using Formula1.Application.Interfaces;
using Formula1.Application.Services;
using Formula1.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;

#region Services

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(VersionService).Assembly));

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

#endregion
#region Build

var app = builder.Build();

app.MapVersionEndpoints();
app.MapDataQueryEndpoints();

app.UseHttpsRedirection();

#endregion
#region Run

app.Run();

public partial class Program { } // For NUnit WebApplication integration tests

#endregion
