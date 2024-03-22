using MyOssHours.Backend.Application;
using MyOssHours.Backend.Infrastructure;
using MyOssHours.Backend.Presentation;
using MyOssHours.Backend.REST.Auth;

var builder = WebApplication.CreateBuilder(args);

// add clean architecture layers
builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddPresentation(builder.Configuration);

builder.Services.AddLogging();
builder.Logging.AddConsole();

// Add services to the container.
builder.AddAuth();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.UseInfrastructure();

app.UseHttpsRedirection();

CookieAuthStartup.UseCookieAuth(app);

app.MapControllers();

app.Run();
