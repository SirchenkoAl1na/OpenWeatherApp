namespace WeatherApp.Entities;

public class WeatherInfo
{
    public int? dt { get; set; }
    public WeatherInfoMain? main { get; set; }
    public string? dt_txt { get; set; }
}