using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Repositories;
using Project_Backend_2024.Services;
using Project_Backend_2024.Services.CommandServices;
using Project_Backend_2024.Services.Interfaces.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.Development.json");

builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection")) //connection string
        );

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserSkillsRepository, UserSkillsRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IAppliedProjectRepository, AppliedProjectRepository>();
builder.Services.AddScoped<IUserSkillsRepository, UserSkillsRepository>();

builder.Services.AddScoped<IUserCommandService, UserCommandService>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Mappers).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
