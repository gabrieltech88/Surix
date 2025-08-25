using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Surix.Api.Data.Models;
using Microsoft.AspNetCore.Identity;
using Surix.Api.Data;
using Microsoft.AspNetCore.StaticFiles;
using Surix.Api.Data.DAL;
using Surix.Api.Services;
using Microsoft.Extensions.FileProviders;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontEnd", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500") // ou "*", se quiser permitir qualquer origem (não recomendado em produção)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

builder.Services.AddDbContext<SurixContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<SurixContext>()
        .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;               // Não exige número
    options.Password.RequireLowercase = false;           // Não exige letra minúscula
    options.Password.RequireUppercase = false;           // Não exige letra maiúscula
    options.Password.RequireNonAlphanumeric = false;     // Não exige caractere especial
    options.Password.RequiredLength = 8;                  // Exige mínimo de 8 caracteres
});

builder.Services.AddAuthentication(options => //LINHA ADICIONADA
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ChaveSecretaSuperSeguraQuePrecisaTerNoMinimo32Caracteres")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["jwt"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }

    };
});


builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SureService>();
builder.Services.AddScoped<SureDAL>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

app.UseCors("PermitirFrontEnd");

var frontPath = Path.Combine(Directory.GetCurrentDirectory(), "../Surix.Front");

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(frontPath),
    RequestPath = "" // raiz do site
});

app.MapControllers();

app.Run();

