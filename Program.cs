using Microsoft.Extensions.DependencyInjection;
using PLE_Ranking.Domain.Entities;
using PLE_Ranking.Domain.Interfaces;
using PLE_Ranking.Domain.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IBolaoService, BolaoService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var myHandlers = AppDomain.CurrentDomain.Load("PLE_Ranking.Domain");
builder.Services.AddMediatR( cfg => cfg.RegisterServicesFromAssemblies(myHandlers));

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
