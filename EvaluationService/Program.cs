using EvaluationService.Data;
using EvaluationService.RabbitMQ;
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

// Add services to the container.
//builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
//{
//    var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
//    return ConnectionMultiplexer.Connect(options);
//});

builder.Services.AddDbContext<ApplicatioDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });



builder.Services.AddHttpClient("Judge0", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:JudgeAPI"])); //add http handler

var optionBuilder = new DbContextOptionsBuilder<ApplicatioDbContext>(); //cant use scoped db on singletoon service

optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(new ResultService(optionBuilder.Options));
builder.Services.AddSingleton<IMQSender, MQSender>();
builder.Services.AddHostedService<MQConsumer>();
//builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
//builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddHttpContextAccessor();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


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


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "lele",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[] {}

        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//SeedDatabase();

app.MapControllers();
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