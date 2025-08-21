using SchoolTransport.Persistence;
using SchoolTransport.Application;
using FluentValidation.AspNetCore;
using SchoolTransport.Application.Exceptions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SchoolTransport.Persistence.Context;
using SchoolTransport.Domain.Entities.Identity;
using SchoolTransport.API.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddControllers
    (
    config =>
    {
        config.CacheProfiles.Add("5mins", new CacheProfile() {Duration=300 });
    }
    );
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("sqlConnection"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "logs",
            AutoCreateSqlTable = true
        },
        columnOptions: new ColumnOptions
        {
            Message = { ColumnName = "message" },
            MessageTemplate = { ColumnName = "message_template" },
            Level = { ColumnName = "level" },
            TimeStamp = { ColumnName = "time_stamp" },
            Exception = { ColumnName = "exception" },
            Properties = { ColumnName = "log_event" },

        })
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();
// Program.cs içinde
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50MB
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "School Transport API",
        Version = "v1"
    });

    // File upload desteði
    options.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer("Admin", options =>
   {
       options.TokenValidationParameters = new()
       {
           ValidateAudience = true, // Token'ýn hangi kitleye/servise yönelik olduðunu doðrular
           ValidateIssuer = true, // Token'ý kimin oluþturduðunu (issuer) doðrular
           ValidateLifetime = true, // Token'ýn süresinin dolup dolmadýðýný kontrol eder
           ValidateIssuerSigningKey = true, // Token'ýn imzalandýðý anahtar ile doðrulanýp doðrulanmadýðýný kontrol eder


           ValidAudience = builder.Configuration["Token:Audience"],
           ValidIssuer = builder.Configuration["Token:Issuer"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),

           LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
           NameClaimType = ClaimTypes.Name



       };
   });
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<SchoolTransportDbContext>()
.AddDefaultTokenProviders();
builder.Services.ConfigurePersistance(builder.Configuration);
builder.Services.ConfigureApplication();
builder.Services.AddValidators();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() // Yerel geliþtirme için tüm origin'lere izin ver (güvenlik için üretimde deðiþtirin, örn: .WithOrigins("http://localhost"))
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});



var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
app.ConfigureExceptionHandler(logger);

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
  

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<NoCacheMiddleware>();

app.MapControllers();

app.Run();
