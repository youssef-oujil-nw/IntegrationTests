using WebApplication1.Aggregates;
using WebApplication1.Aggregates.Enums;

namespace WebApplication1.Services.Interface;

public interface IWeatherService
{
    WeatherStatus GetWeatherStatus(int degree);
}