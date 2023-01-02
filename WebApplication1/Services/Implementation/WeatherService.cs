using WebApplication1.Aggregates;
using WebApplication1.Aggregates.Enums;
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

    public Task<Guid> CreateWeatherReport(WeatherReport weatherReport)
    {
        return _weatherRepository.CreateWeaTheReport(weatherReport);
    }
}