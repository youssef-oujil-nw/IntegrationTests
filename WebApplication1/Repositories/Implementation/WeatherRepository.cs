using Microsoft.EntityFrameworkCore;
using WebApplication1.Aggregates;
using WebApplication1.Aggregates.Enums;
using WebApplication1.Entities;
using WebApplication1.Repositories.Context;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories.Implementation;

public class WeatherRepository:IWeatherRepository
{
    private readonly WeatherDbContext _weatherDbContext;

    public WeatherRepository(WeatherDbContext weatherDbContext)
    {
        _weatherDbContext = weatherDbContext;
    }

    public async Task<IEnumerable<WeatherReport>> GetWeatherByLocation(Location location)
    {
        var weatherReportEntities =
            await _weatherDbContext.WeatherReport.AsNoTracking().Where(entity => entity.Location == location).ToListAsync();
        var weatherReports = weatherReportEntities.Select(reportEntities =>
            new WeatherReport(reportEntities.Location, reportEntities.Degree,
                reportEntities.Status, reportEntities.Time));
        return weatherReports;
    }

    public async Task<Guid> CreateWeaTheReport(WeatherReport weatherReport)
    {
        var weatherReportEntity =
            new WeatherReportEntity(weatherReport.Location, weatherReport.Degree, weatherReport.Status,weatherReport.Time);
        var entry = await _weatherDbContext.WeatherReport.AddAsync(weatherReportEntity);
        await _weatherDbContext.SaveChangesAsync();
        entry.State = EntityState.Detached; 
        return entry.Entity.Id;
    }
}