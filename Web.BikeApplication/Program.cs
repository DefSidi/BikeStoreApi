using Library.BikeApplication.Interface;
using Library.BikeApplication;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile("logs/log.txt");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
//builder.Services.AddLogging(builder =>
//{
//    builder.AddConfiguration(Configuration.GetSection("Logging"));
//    builder.AddConsole();
//    builder.AddDebug();
//    builder.AddFile(); // Add file logging provider
//});
builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<ILogger , Logger<BikeService>>();
builder.Services.AddScoped<ILogger, Logger<BikeRepository>>();
builder.Services.AddScoped<IBikeRepository, BikeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
