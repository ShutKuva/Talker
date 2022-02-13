using BLL;
using DAL.EFContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<TalkerDbContext>(options => options
    .UseSqlServer(
        builder
            .Configuration
            .GetSection("SQLDBConnection")
            .GetSection("ConnectionString")
            .Value
    ));
DependencyRegistrar.ConfigureServices(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
