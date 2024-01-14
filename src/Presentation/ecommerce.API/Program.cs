using ecommerce.Application;
using ecommerce.Persistence;
using ecommerce.Persistence.Seeding;

var builder = WebApplication.CreateBuilder(args);

//~ Begin - Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
//~ End

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//~ Begin - Seed data
await Seeding.SeedInitialRoles(app.Services);
//~ End

app.Run();
