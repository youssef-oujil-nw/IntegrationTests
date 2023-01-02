using WebApplication1.Aggregates;
using WebApplication1.Aggregates.Enums;

namespace WebApplication1.Repositories.Interfaces;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherReport>> GetWeatherByLocation(Location location);
    Task<Guid> CreateWeaTheReport(WeatherReport weatherReport);
}