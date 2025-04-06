using ChatQueue.Services;

namespace ChatQueue.Worker;

public class AgentCoordinatorWorker : BackgroundService
{
    private readonly ILogger<AgentCoordinatorWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    public AgentCoordinatorWorker(ILogger<AgentCoordinatorWorker> logger, IServiceProvider serviceProvider)
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

                queueService.AssignToAgent();
            }
            await Task.Delay(10000, stoppingToken); // Delay for 10 seconds to simulate worker interval
        }
    }

}
