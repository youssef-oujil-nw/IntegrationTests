using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Repositories.Context;


public class WeatherDbContext:DbContext
{
    private readonly IConfiguration _configuration;
    
    public DbSet<WeatherReportEntity> WeatherReport => Set<WeatherReportEntity>();

    public WeatherDbContext(IConfiguration dbConfiguration, DbContextOptions<WeatherDbContext> options)
    {
        _configuration = dbConfiguration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("WeatherContext"));
        }
    }
   

}