using Formula1.Api.Endpoints;
using Formula1.Api.Extensions;
using Formula1.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;

#region Services

var builder = WebApplication.CreateBuilder(args);

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

app.MapAliveEndpoints();
app.MapCircuitsEndpoints();
app.MapConstructorsEndpoints();
app.MapDriversEndpoints();
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
