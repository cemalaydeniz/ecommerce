using ecommerce.Application;
using ecommerce.Persistence;
using ecommerce.Persistence.Authentication;
using ecommerce.Persistence.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//~ Begin - Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

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

app.MapControllers();

//~ Begin - Seed data
await Seeding.SeedInitialRoles(app.Services);
//~ End

app.Run();
