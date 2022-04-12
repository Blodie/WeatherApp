using WeatherApp.Main.Services.Interfaces;

namespace WeatherApp.Main.Services.BackgroundServices;

public class ForecastUpdaterService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private const int MS_IN_A_DAY = 24 * 60 * 60 * 1000;

    public ForecastUpdaterService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var weatherService = scope.ServiceProvider.GetService<IWeatherService>();
        var forecastUpdateFreqMS = scope.ServiceProvider.GetService<IConfiguration>().GetSection("ForecastUpdateFreqDays")?.Get<int>() * MS_IN_A_DAY ?? MS_IN_A_DAY;

        while (!stoppingToken.IsCancellationRequested)
        {
            await weatherService.UpdateAllForecasts();
            await Task.Delay(forecastUpdateFreqMS, stoppingToken);
        }
    }

}
