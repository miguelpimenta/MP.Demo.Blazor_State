using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using MP.BlazorStateDemo.Core.Domain.Entities;
using System.Net.Http.Json;

namespace MP.BlazorStateDemo.Components;

public partial class WeatherForecastComponent : ComponentBase
{
    [Inject]
    private HttpClient Http { get; set; }

    protected async ValueTask<ItemsProviderResult<WeatherForecast>> LoadForecasts(
        ItemsProviderRequest request)
    {
        var forecasts = await Http
            .GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");

        return new ItemsProviderResult<WeatherForecast>(
            forecasts.Skip(request.StartIndex).Take(request.Count), forecasts.Count());
    }
}