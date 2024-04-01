using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Data;
using Backend.Controllers;
using Backend.ServiceLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura tu DbContext
            var serviceProvider = new ServiceCollection()
                .AddDbContext<CineContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")))
                .BuildServiceProvider();
            
            // Comprueba si la base de datos existe
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CineContext>();
                if (dbContext.Database.CanConnect())
                {
                    Console.WriteLine("La base de datos ya existe en SQl.");
                }
                else
                {
                    Console.WriteLine("La base de datos no existe todavía.");

                    // Aplica las migraciones si la base de datos no existe
                    dbContext.Database.Migrate();
                    Console.WriteLine("Migraciones aplicadas exitosamente.");
                }
            }

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

//UserAutorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
(options => options.TokenValidationParameters = new TokenValidationParameters 
                        {ValidateIssuerSigningKey=true,
                         IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                         ValidateIssuer=false,
                         ValidateAudience= false});

builder.Services.AddAuthorization(option=>{option.AddPolicy("SuperAdmin",policy=>policy.RequireClaim("AdminType","Admin"));
                                    });


// Agrega esto al método ConfigureServices en Startup.cs
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5174")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();
// Agrega esto al método Configure en Startup.cs
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();