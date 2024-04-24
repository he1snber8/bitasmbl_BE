using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer("server = database-1.chk6gakouzh7.eu-central-1.rds.amazonaws.com,1433; User Id=admin;Password=meditations;Database = mydatabase;TrustServerCertificate  = true") //connection string
        );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
