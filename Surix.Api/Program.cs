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
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<SurixContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<SurixContext>()
        .AddDefaultTokenProviders();

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
		options.DefaultAuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
{
		options.TokenValidationParamaters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters 
		{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ChaveSecretaSuperSeguraQuePrecisaTerNoMinimo32Caracteres")),
				ValidateAudience = false,
				ValidateIssuer = false,
				ClockSkew = TimeSpan.Zero
		};
});


builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SureDAL>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    // Verifica se a URL termina sem extensão e se o arquivo .html correspondente existe
    if (!Path.HasExtension(context.Request.Path.Value))
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "Surix.Front", context.Request.Path.Value.TrimStart('/') + ".html");

        if (System.IO.File.Exists(path))
        {
            context.Request.Path = new PathString(context.Request.Path.Value + ".html");
        }
    }

    await next();
});

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "..", "Surix.Front")
    ),
    RequestPath = "", // raiz do site
    EnableDefaultFiles = true
});

app.UseHttpsRedirection();
app.UseDefaultFiles(); // Procura automaticamente por index.html
app.UseStaticFiles();
app.MapControllers();

app.Run();

