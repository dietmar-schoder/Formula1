using Formula1.Api.Endpoints;
using Formula1.Api.Extensions;
using Formula1.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

#region Services

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

#endregion
#region Build

var app = builder.Build();

app.MapVersionEndpoints();
app.MapDataImportEndpoints();

app.UseHttpsRedirection();

#endregion
#region Run

app.Run();

public partial class Program { } // For NUnit WebApplication integration tests

#endregion
