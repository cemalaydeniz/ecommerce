using ecommerce.API.Events.Jwt;
using ecommerce.API.Filters;
using ecommerce.API.Middlewares;
using ecommerce.Application;
using ecommerce.Persistence;
using ecommerce.Persistence.Authentication;
using ecommerce.Persistence.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//~ Begin - Services
builder.Services.AddControllers(_ =>
{
    _.Filters.Add(typeof(ReformatValidationProblemFilter));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(_ =>
{
    _.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "ecommerce",
        Version = "v1"
    });

    _.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Description = "Jwt Bearer Token",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header
    });

    _.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program).GetTypeInfo().Assembly);

var issuer = builder.Configuration[JwtConstants.Issuer_ConfigKey];
var audience = builder.Configuration[JwtConstants.Audience_ConfigKey];
var jwtSecretKey = builder.Configuration[JwtConstants.Key_ConfigKey];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(_ =>
{
    _.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey!)),
        ClockSkew = TimeSpan.Zero
    };
    _.Events = new JwtBearerEvents
    {
        OnChallenge = ReformatUnauthorized.Handle,
        OnForbidden = ReformatForbidden.Handle
    };
});
//~ End

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

//~ Begin - Seed data
await Seeding.SeedInitialRoles(app.Services);
//~ End

app.Run();
