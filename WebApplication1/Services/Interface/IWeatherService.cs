using WebApplication1.Aggregates;
using WebApplication1.Aggregates.Enums;

namespace WebApplication1.Services.Interface;

public interface IWeatherService
{
    Task<IEnumerable<WeatherReport>> GetLocationWeatherReports(Location location);
    Task<Guid> CreateWeatherReport(WeatherReport weatherReport);
}