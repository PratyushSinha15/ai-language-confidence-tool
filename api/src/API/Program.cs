using System.Text;
using Repository;
using Repository.IRepository;
using API.Services.IServices;
using Business;
using Business.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;

var builder = WebApplication.CreateBuilder(args);

//http client for ollama
builder.Services.AddHttpClient<IOllamaService, OllamaService>(client =>
{
    client.Timeout = TimeSpan.FromMinutes(10);
});

// Controllers
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient",
        policy =>
        {
            policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });


// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAnalysisRepository, AnalysisRepository>();


// Services
builder.Services.AddScoped<IAuthService, AuthServices>();
builder.Services.AddScoped<IJwtService, JwtService>();
// builder.Services.AddScoped<IOllamaService, OllamaService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();


var app = builder.Build();

// Middlewares
// app.UseHttpsRedirection();
app.UseCors("AngularClient");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();