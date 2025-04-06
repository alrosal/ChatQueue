namespace ChatQueue.Services;

public interface IChatQueueService
{
    bool AddToQueue(string sessionId);
    void AssignToAgent();
    void PollSessions();
}
