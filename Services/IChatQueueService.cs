namespace ChatQueue.Services;

public interface IChatQueueService
{
    bool AddToQueue(string sessionId);
}
