using System.Text.Json.Serialization;
using WebApplication1.Repositories.Context;
using WebApplication1.Repositories.Implementation;
using WebApplication1.Repositories.Interfaces;
using WebApplication1.Services.Implementation;
using WebApplication1.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddDbContext<WeatherDbContext>();

builder.Services.AddScoped<IWeatherService,WeatherService>();
builder.Services.AddScoped<IWeatherRepository,WeatherRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WeatherDbContext>();
    context.Database.EnsureCreated();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
public partial class Program { }
