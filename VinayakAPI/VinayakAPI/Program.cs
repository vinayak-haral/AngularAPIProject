using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using System.Text;
using VinayakAPI.Data;
using VinayakAPI.Interfaces;
using VinayakAPI.Repository;

using Serilog;
using VinayakAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//--------------------------------------------
    
// Register logging service
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();  // You can add other providers as needed

///
// Configure Serilog it will logs the loggers in below folder or files
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.Console()
.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();


builder.Host.UseSerilog();

//builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Register repository
//builder.Services.AddScoped<IUserRepository, UserRegistRepository>();
builder.Services.AddScoped<ILogin, LoginRepository>();

builder.Services.AddSingleton<TokenService>(); // Register Token service here

// Configure DbContext with connection string
builder.Services.AddDbContext<mainAPIDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FullStackConnectionString")));


// Add Authentication services
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("SecretKey"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        //ValidAudience = jwtSettings.GetValue<string>("Audience"),
        //IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

builder.Services.AddAuthorization();

//--------------------------------------------
var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("This is an information log with Serilog.");
    return "Check your Serilog file!";
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

// Run the application
app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

//app.UseAuthorization();
// Add Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
