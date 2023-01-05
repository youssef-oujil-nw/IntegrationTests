using WebApplication1.Aggregates;
using WebApplication1.Aggregates.Enums;
using WebApplication1.Commands;

namespace WebApplication1.Services.Interface;

public interface IWeatherService
{
    Task<IEnumerable<WeatherReport>> GetLocationWeatherReports(Location location);
    Task<Guid> CreateWeatherReport(CreateWeatherReportCommand weatherReportCommand);
}