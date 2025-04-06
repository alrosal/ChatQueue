using ChatQueue.Services;

namespace ChatQueue.Worker;

public class AgentCoordinatorWorker : BackgroundService
{
    private readonly ILogger<AgentCoordinatorWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public AgentCoordinatorWorker(ILogger<AgentCoordinatorWorker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timeDelay = _configuration.GetValue<int?>("Application:AssignAgentTimeDelay") ?? 10000;

        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var queueService = scope.ServiceProvider.GetRequiredService<IChatQueueService>();

                queueService.AssignToAgent();
            }
            await Task.Delay(timeDelay, stoppingToken); // Delay for (Default)10 seconds to simulate worker interval
        }
    }

}
