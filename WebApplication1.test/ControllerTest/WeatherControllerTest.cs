using Newtonsoft.Json.Linq;
using WebApplication1.Aggregates.Enums;
using WebApplication1.DTOs;
using WebApplication1.test.Factory;
using Xunit;

namespace WebApplication1.test.ControllerTest;

public class WeatherControllerTest:IClassFixture<WebClientFactory>
{
    private readonly HttpClient _client;

    public WeatherControllerTest(WebClientFactory webClientFactory)
    {
        _client = webClientFactory.CreateClient();
    }

    [Theory]
    [InlineData(-10,WeatherStatus.Freezing)]
    [InlineData(5,WeatherStatus.Cold)]
    [InlineData(22,WeatherStatus.JustRight)]
    [InlineData(32,WeatherStatus.Warm)]
    [InlineData(40,WeatherStatus.Hot)]
    public async Task Degrees_ShouldReturnTheRightStatus(int degree,WeatherStatus weatherStatus)
    {
        //Act
        var response = await _client.GetAsync($"{degree}");
        var responseString = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        var responseObject = JObject.Parse(responseString).ToObject<GetWeatherStatusResponse>();
        // Assert
       
        Assert.NotNull(responseObject);
        Assert.Equal(weatherStatus, responseObject.WeatherStatus);

    }

}