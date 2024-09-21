using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using System.Text;
using VinayakAPI.Data;
using VinayakAPI.Interfaces;
using VinayakAPI.Repository;

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


//builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Register repository
builder.Services.AddSingleton<IUserRepository, UserRegistRepository>();

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
        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        ValidAudience = jwtSettings.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

builder.Services.AddAuthorization();

//--------------------------------------------
var app = builder.Build();

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
