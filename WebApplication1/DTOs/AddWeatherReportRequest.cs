using WebApplication1.Aggregates.Enums;

namespace WebApplication1.DTOs;

public record AddWeatherReportRequest(int Degree,DateTime Time,Location Location);