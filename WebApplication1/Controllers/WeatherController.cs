using Microsoft.AspNetCore.Mvc;
using WebApplication1.Aggregates.Enums;
using WebApplication1.Commands;
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
    [Route("{location}")]
    public async Task<IEnumerable<GetWeatherStatusResponse>> GetWeatherStatus(Location location)
    {
        var weatherReports = await _weatherService.GetLocationWeatherReports(location);
        return weatherReports.Select( weatherReport => new GetWeatherStatusResponse(weatherReport.Status,weatherReport.Degree,weatherReport.Time));
    }    
    
    [HttpPost]
    [Route("")]
    public async Task<AddWeatherReportResponse> GetWeatherStatus([FromBody] AddWeatherReportRequest addWeatherReportRequest)
    {
        var createWeatherReportCommand = new CreateWeatherReportCommand(addWeatherReportRequest.Degree,
            addWeatherReportRequest.Time, addWeatherReportRequest.Location);
        var weatherReportId = await _weatherService.CreateWeatherReport(createWeatherReportCommand);
        return new AddWeatherReportResponse(weatherReportId);
    }
}