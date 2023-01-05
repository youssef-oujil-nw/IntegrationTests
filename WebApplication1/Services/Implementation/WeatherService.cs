using WebApplication1.Aggregates;
using WebApplication1.Aggregates.Enums;
using WebApplication1.Commands;
using WebApplication1.Repositories.Interfaces;
using WebApplication1.Services.Interface;

namespace WebApplication1.Services.Implementation;

public class WeatherService:IWeatherService
{
    private readonly IWeatherRepository _weatherRepository;

    public WeatherService(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public WeatherStatus GetWeatherStatus(int degree)
    {
        if (degree <= 0)
        {
            return WeatherStatus.Freezing;
        }

        if (degree <= 20)
        {
            return WeatherStatus.Cold;
        }

        if (degree <= 25)
        {
            return WeatherStatus.JustRight;
        }
         if (degree <= 35)
        {
            return WeatherStatus.Warm;
        } 
        return WeatherStatus.Hot;

    }


    public Task<IEnumerable<WeatherReport>> GetLocationWeatherReports(Location location)
    {
        return _weatherRepository.GetWeatherByLocation(location);
    }

    public async Task<Guid> CreateWeatherReport(CreateWeatherReportCommand weatherReportCommand)
    {
        var weatherStatus = GetWeatherStatus(weatherReportCommand.Degree);
        var weatherReport = new WeatherReport(weatherReportCommand.Location, weatherReportCommand.Degree, weatherStatus,
            weatherReportCommand.Time);
        return await _weatherRepository.CreateWeaTheReport(weatherReport);
    }
}