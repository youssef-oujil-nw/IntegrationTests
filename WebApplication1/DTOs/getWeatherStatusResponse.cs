using WebApplication1.Aggregates.Enums;

namespace WebApplication1.DTOs;

public record GetWeatherStatusResponse(WeatherStatus WeatherStatus, int Degree,DateTime Time);