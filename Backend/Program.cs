using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Data;
using Backend.Controllers;
using Backend.ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<CineContext>(builder.Configuration.GetConnectionString("ApplicationDbContext"));

//Service Layer
builder.Services.AddScoped<ServiceActor>();
builder.Services.AddScoped<ServiceButaca>();
builder.Services.AddScoped<ServiceCliente>();
builder.Services.AddScoped<ServiceCompra>();
builder.Services.AddScoped<ServiceDescuento>();
builder.Services.AddScoped<ServiceEfectivo>();
builder.Services.AddScoped<ServiceGenero>();
builder.Services.AddScoped<ServicePago>();
builder.Services.AddScoped<ServicePelicula>();
builder.Services.AddScoped<ServicePunto>();
builder.Services.AddScoped<ServiceSala>();
builder.Services.AddScoped<ServiceSesion>();
builder.Services.AddScoped<ServiceTarjetum>();
builder.Services.AddScoped<ServiceUsuario>();
builder.Services.AddScoped<ServiceWeb>();

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
