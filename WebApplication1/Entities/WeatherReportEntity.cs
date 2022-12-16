using WebApplication1.Aggregates.Enums;

namespace WebApplication1.Entities;

public class WeatherReportEntity
{
    public Guid Id { get; set; }
    public Location Location { get; set; }
    public int Degree { get; set; }
    public WeatherStatus Status { get; set; }
    public DateTime Time { get; set; }
}