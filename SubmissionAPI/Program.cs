using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using SubmissionAPI.Data;
using SubmissionAPI.Data.Initialize;
using SubmissionAPI.RabbitMQ;
using SubmissionAPI.Service;
using SubmissionAPI.Service.IService;
using System.Text;
using Xphyrus.MessageBus;


using Xphyrus.SubmissionAPI.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(options);
});

builder.Services.AddDbContext<ApplicatioDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BackendApiAuthHttpClientHandler>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>(); 

builder.Services.AddScoped<IJudgeService, JudgeService>();
//builder.Services.AddScoped<IBus, Bus>();
builder.Services.AddScoped<IMQSender, MQSender>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Judge0", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:JudgeAPI"])).AddHttpMessageHandler<BackendApiAuthHttpClientHandler>(); //add http handler


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//authen before authorizations
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        //what it checking agaist
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtOptions:Secret"))),
        ValidIssuer = builder.Configuration.GetValue<string>("JwtOptions:Issuer"),
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidAudience = builder.Configuration.GetValue<string>("JwtOptions:Audience")
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

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseCors("CorsPolicy");

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

SeedDatabase();

app.MapControllers();

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize(app.Environment.IsProduction());
    }
}