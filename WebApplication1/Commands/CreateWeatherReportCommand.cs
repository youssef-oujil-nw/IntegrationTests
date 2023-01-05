using WebApplication1.Aggregates.Enums;

namespace WebApplication1.Commands;

public record CreateWeatherReportCommand(int Degree, DateTime Time, Location Location);
