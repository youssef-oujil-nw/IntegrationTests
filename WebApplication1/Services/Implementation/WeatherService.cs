using WebApplication1.Aggregates;
using WebApplication1.Aggregates.Enums;
using WebApplication1.Services.Interface;

namespace WebApplication1.Services.Implementation;

public class WeatherService:IWeatherService
{
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


}