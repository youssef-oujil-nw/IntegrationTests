using WebApplication1.Aggregates.Enums;

namespace WebApplication1.Entities;

public class WeatherReportEntity
{
    public WeatherReportEntity(Location location, int degree, WeatherStatus status, DateTime time)
    {
        Location = location;
        Degree = degree;
        Status = status;
        Time = time;
    } public WeatherReportEntity(Guid id, Location location, int degree, WeatherStatus status, DateTime time)
    {
        Id = id;
        Location = location;
        Degree = degree;
        Status = status;
        Time = time;
    }

    public Guid Id { get; set; }
    public Location Location { get; set; }
    public int Degree { get; set; }
    public WeatherStatus Status { get; set; }
    public DateTime Time { get; set; }
}