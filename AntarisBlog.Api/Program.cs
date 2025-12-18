using AntarisBlog.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
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


