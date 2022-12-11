using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services.Interface;

namespace WebApplication1.Controllers;

[ApiController]
public class WeatherController:ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    [Route("{degree}")]
    public GetWeatherStatusResponse GetWeatherStatus(int degree)
    {
        var weatherStatus = _weatherService.GetWeatherStatus(degree);
        return new GetWeatherStatusResponse(weatherStatus);
    }
}