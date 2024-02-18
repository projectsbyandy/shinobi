using Shinobi.Core.Data;
using Shinobi.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = ConfigExtensions.GetConfiguration();
builder.Services.AddConfigurationSupport(config);
builder.Services.AddSingleton<ShinobiContext>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
