using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepoLayer;
using QuantityMeasurementRepoLayer.Config;
using QuantityMeasurementRepoLayer.Interfaces;
using QuantityMeasurementRepoLayer.Repositories;
using QuantityMeasurementRepoLayer.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

//connection string
string conn = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddDbContext<MeasurementDbContext>(option => option.UseSqlServer(conn));
builder.Services.AddSingleton<ICacheRepository, InMemoryCacheRepository>();
builder.Services.AddScoped<IDatabaseRepository,DatabaseRepository>();

builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
