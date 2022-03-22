using BLL;
using Core;
using DAL.EFContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TalkerAPI.Authorization;
using TalkerAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<JwtParams>(builder.Configuration.GetSection("API"));

builder.Services.AddDbContext<TalkerDbContext>(options => options
    .UseSqlServer(
        builder
            .Configuration
            .GetSection("SQLDBConnection")
            .GetSection("ConnectionString")
            .Value
    ));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.RequireHttpsMetadata = false;
    }

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetSection("API").GetValue<string>("Issuer"),

        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("API").GetValue<string>("Audience"),
        ValidateLifetime = true,
        //LifetimeValidator = delegate (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        //{
        //    //JwtSecurityToken token = (JwtSecurityToken)securityToken;
        //    //if (DateTime.Now.CompareTo(notBefore) < 0)
        //    //{
        //    //    return false;
        //    //} else if (DateTime.Now.Equals(expires) || DateTime.Now.CompareTo(expires) > 0)
        //    //{
        //    //    return false;
        //    //}
        //    //return true;
            
        //    return Validators.ValidateLifetime(notBefore, expires, securityToken, validationParameters);
        //},

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("API").GetValue<string>("Key"))),
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("try", configure =>
    {
        configure.RequireClaim("username");
    });
});

//options =>
//{
//    var builder = new AuthorizationPolicyBuilder();
//    options.DefaultPolicy = builder.RequireClaim("username").Build();
//}

DependencyRegistrar.ConfigureServices(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
//app.UseAuthorization();
app.UseMiddleware<JwtAuthenticateMiddleware>();

app.MapControllers();

app.Run();
