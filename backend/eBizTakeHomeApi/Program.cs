var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers(); // If using controllers

var app = builder.Build();

// Enable the CORS policy globally
app.UseCors("AllowAll");

app.MapControllers(); // If using controllers

app.Run();
