////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.
////// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
////builder.Services.AddOpenApi();

////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.MapOpenApi();
////}

////app.UseHttpsRedirection();

////var summaries = new[]
////{
////    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
////};

////app.MapGet("/weatherforecast", () =>
////{
////    var forecast =  Enumerable.Range(1, 5).Select(index =>
////        new WeatherForecast
////        (
////            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
////            Random.Shared.Next(-20, 55),
////            summaries[Random.Shared.Next(summaries.Length)]
////        ))
////        .ToArray();
////    return forecast;
////})
////.WithName("GetWeatherForecast");

////app.Run();

////record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
////{
////    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
////}

//using AntarisBlog.Api.Data;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//// We will use controllers (PostsController, CommentsController, etc.)
//builder.Services.AddControllers();

//// OpenAPI (from the .NET 9 template) – keeps /openapi/v1.json
//builder.Services.AddOpenApi();

//// CORS for Angular client
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularClient", policy =>
//    {
//        policy
//            .WithOrigins("http://localhost:4200") // Angular dev server
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

//// DbContext + SQL Server
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
//                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//builder.Services.AddDbContext<AntarisBlogContext>(options =>
//    options.UseSqlServer(connectionString));

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    // Exposes OpenAPI description at /openapi/v1.json
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//// Enable CORS
//app.UseCors("AllowAngularClient");

//// Map attribute-routed controllers (e.g., [Route("api/[controller]")])
//app.MapControllers();

//app.Run();

using AntarisBlog.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// New .NET 9 OpenAPI description (JSON at /openapi/v1.json in Development)
builder.Services.AddOpenApi();

// CORS for Angular client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") // Angular dev server
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// DbContext + SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AntarisBlogContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Exposes OpenAPI JSON at /openapi/v1.json
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAngularClient");

app.UseAuthorization();

// Map attribute-routed controllers (e.g. [Route("api/[controller]")])
app.MapControllers();

app.Run();


