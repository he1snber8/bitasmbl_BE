
using Project_Backend_2024;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog(builder.Configuration);
builder.Services.ConfigureApp(builder.Configuration);

var app = builder.Build();
app.AddSwagger();
app.AddMiddleWare();
app.Run();