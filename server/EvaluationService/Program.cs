using EvaluationService.Data;
using EvaluationService.Data.Initialize;
using EvaluationService.RabbitMQ;
using EvaluationService.Service;
using EvaluationService.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.Text;
using Xphyrus.EvaluationAPI.RabbitMQ;
using Xphyrus.EvaluationAPI.Service;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
return ConnectionMultiplexer.Connect(options);
});

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });





var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>(); //cant use scoped db on singletoon service

optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(new ResultService(optionBuilder.Options));
builder.Services.AddSingleton<ICachingService, CachingService>();

builder.Services.AddSingleton<IMQSender, MQSender>();

builder.Services.AddHostedService<MQConsumer>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddHttpContextAccessor();





//authen before authorizations
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                //what it checking agaist
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Secret"])),
                ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = false
            };
        });
builder.Services.AddAuthorization();




var app = builder.Build();

// Configure the HTTP request pipeline.



app.UseAuthentication();
app.UseAuthorization();

//SeedDatabase();


//app.UseAzureServiceBusConsumer();
app.Run();


//void SeedDatabase()
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
//        dbInitializer.Initialize(app.Environment.IsProduction());
//    }
//}