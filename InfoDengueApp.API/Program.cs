using InfoDengueApp.Infra.Data.Extensions;
using InfoDengueApp.Application.Interfaces;
using InfoDengueApp.Application.Interfaces.ExternalServices;
using InfoDengueApp.Application.Services;
using InfoDengueApp.Application.Services.External;
using InfoDengueApp.Domain.Interfaces.Core;
using InfoDengueApp.Domain.Interfaces.Repository;
using InfoDengueApp.Infra.Data.ExternalServices;
using InfoDengueApp.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog - Gravar os logs 
Directory.CreateDirectory(@"C:\Estudo\Logs");

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Controllers
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Entity Framework e Infra
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddInfrastructureServices();

// Repositórios
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();

// Serviços
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPerfilService, PerfilService>();

// Serviço para integração com API externa InfoDengue
builder.Services.AddHttpClient<IInfoDengueService, InfoDengueService>();

// Serviço Dados Epidemiológicos
builder.Services.AddScoped<IDadosEpidemiologicosService, DadosEpidemiologicosService>();

// JWT
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

// Swagger com JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InfoDengueApp", Version = "v1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Informe o token JWT no formato: Bearer {seu token}",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// App
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapScalarApiReference(options =>
{
    options.WithTheme(ScalarTheme.BluePlanet);
});

// Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
