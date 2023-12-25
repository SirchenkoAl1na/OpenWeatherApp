namespace WeatherApp.Entities;

public class City
{
    public string? name { get; set; }
    public Coord? coord { get; set; }
    public string? country { get; set; }
    public int? population { get; set; }
    public int? timezone { get; set; }
    public int? sunrise { get; set; }
    public int? sunset { get; set; }
}