using WorkerService1.Logger;

namespace WorkerService1;

public class Worker(ILoggerAdapter<Worker> loggerAdapter) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            loggerAdapter.LogInformation(LogState.Success, LogAction.Update, "Worker running at: {time} and Hello to {name}", DateTimeOffset.Now, "Roi");

            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
        }
    }
}