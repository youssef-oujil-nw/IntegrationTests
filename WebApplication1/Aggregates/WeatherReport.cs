using WebApplication1.Aggregates.Enums;

namespace WebApplication1.Aggregates;

public class WeatherReport
{
    public WeatherReport(Location location, int degree, WeatherStatus status, DateTime time)
    {
        Location = location;
        Degree = degree;
        Status = status;
        Time = time;
    }

    public Location Location { get; set; }
    public int Degree { get; set; }
    public WeatherStatus Status { get; set; }
    public DateTime Time { get; set; }
}