using CodeDialect.Infrastructure;
using CodeDialect.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();

// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CodeDialect.Infrastructure.Persistence.InitialSeed.SeedAsync(services);
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
