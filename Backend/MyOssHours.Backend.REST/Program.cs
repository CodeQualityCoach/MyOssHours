using MyOssHours.Backend.REST.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

CookieAndHtaccessAuthStartup.AddCookieAuth(builder.Services);

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

app.UseHttpsRedirection();

CookieAndHtaccessAuthStartup.UseCookieAuth(app);

app.MapControllers();

app.Run();
