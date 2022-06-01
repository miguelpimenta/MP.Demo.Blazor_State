using MP.BlazorStateDemo.Core.Domain.Common;

namespace MP.BlazorStateDemo.Core.Domain.Entities;

public class WeatherForecast : BaseEntity
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}