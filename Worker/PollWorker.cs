using ChatQueue.Services;

namespace ChatQueue.Worker;

public class PollWorker : BackgroundService
{
    private readonly ILogger<PollWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    public PollWorker(ILogger<PollWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var queueService = scope.ServiceProvider.GetRequiredService<IChatQueueService>();
                //queueService.PollQueue();
            }
            await Task.Delay(Constants.POLLING_INTERVAL, stoppingToken);
        }
    }
}
