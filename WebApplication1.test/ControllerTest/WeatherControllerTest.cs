using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.Aggregates.Enums;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories.Context;
using WebApplication1.test.Factory;
using Xunit;

namespace WebApplication1.test.ControllerTest;

public class WeatherControllerTest:IClassFixture<WebClientFactory>
{
    private readonly HttpClient _client;
    private readonly WeatherDbContext _weatherDbContext;


    public WeatherControllerTest(WebClientFactory webClientFactory)
    {
        _client = webClientFactory.CreateClient();
        var scope = webClientFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _weatherDbContext = scope.ServiceProvider.GetRequiredService<WeatherDbContext>() ?? throw new ArgumentNullException(nameof(_weatherDbContext), "couldn't instantiate");
    }

    [Fact]
    public async Task GetWeatherReport_ShouldReturnTheRightReports()
    {
        //Arrange
        var expectedResponse =
            new GetWeatherStatusResponse( WeatherStatus.Hot,32 , new DateTime(2021, 2, 22));
        var weatherReportEntities = new List<WeatherReportEntity>
        {
            new(Location.California, 32, WeatherStatus.Hot, new DateTime(2021, 2, 22)),
            new(Location.Atlanta, 12, WeatherStatus.Cold, new DateTime(2000, 12, 12))
        };
        await _weatherDbContext.WeatherReport.AddRangeAsync(weatherReportEntities);
        await _weatherDbContext.SaveChangesAsync();

        //Act
        var response = await _client.GetAsync(Location.California.ToString());
        var responseString = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        await _weatherDbContext.WeatherReport.CountAsync();
        var responseObject = JArray.Parse(responseString).ToObject<List<GetWeatherStatusResponse>>();
        // Assert
        Assert.NotNull(responseObject);
        Assert.Single(responseObject);
        Assert.Equivalent(expectedResponse,responseObject.First());
    }    
    
    [Fact]
    public async Task PostWeatherReport_Should_AddTheWeatherReport()
    {
        //Arrange
        var date = new DateTime(2021, 2, 22);
        var weatherReportRequest = new AddWeatherReportRequest( 32 ,date,Location.Atlanta);
        var weatherRequest = new StringContent(JsonConvert.SerializeObject(weatherReportRequest), Encoding.UTF8)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        };
        //Act
        var response = await _client.PostAsync("",weatherRequest);

        var responseString = await response.Content.ReadAsStringAsync();
        var responseObject = JObject.Parse(responseString).ToObject<AddWeatherReportResponse>();


        var responseGuid = responseObject?.WeaTheReportId;

        response.EnsureSuccessStatusCode();
        await _weatherDbContext.WeatherReport.CountAsync();
        var weatherReportEntity =await _weatherDbContext.WeatherReport.FirstOrDefaultAsync(report => report.Id == responseGuid);
        //Assert

        Assert.NotNull(responseObject);

        var expectedWeatherResult = new WeatherReportEntity(responseObject.WeaTheReportId, Location.Atlanta,32, WeatherStatus.Warm, date);
        Assert.Equivalent(expectedWeatherResult,weatherReportEntity);

    }

}