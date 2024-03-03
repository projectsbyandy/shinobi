using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Shinobi.Core.Data;
using Shinobi.Core.Extensions;
using Shinobi.Core.Models.Config;
using Shinobi.Core.Repositories;
using Shinobi.Core.Repositories.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

var config = ConfigExtensions.GetConfiguration();
var dbConfiguration = config.GetSection("DbConfiguration").Get<DbConfiguration>();
Guard.Against.Null(dbConfiguration?.SqlConnectionDetails);

builder.Services.AddTransient<INinjaRepository, NinjaRepository>();
builder.Services.AddDbContext<ShinobiDbContext>(options =>
{
    if (dbConfiguration.UseMock)
    {
        options.UseInMemoryDatabase("shinobi");
    }
    else
    {
        options.UseSqlServer(
            $"Server={dbConfiguration.SqlConnectionDetails.Server}; Initial Catalog={dbConfiguration.SqlConnectionDetails.Catalog}; " +
            $"user={dbConfiguration.SqlConnectionDetails.UserName};Password={dbConfiguration.SqlConnectionDetails.Password};TrustServerCertificate=True;");   
    }
});

builder.Services.AddControllers();

var app = builder.Build();

if (dbConfiguration.UseMock)
{
    using var scope = app.Services.CreateScope();
    MockDataGenerator.Initialize(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
