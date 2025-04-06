using ChatQueue.Mocker;
using ChatQueue.Models;

namespace ChatQueue.Services;

public class ChatQueueService : IChatQueueService
{
    private readonly Queue<ChatSessionViewModel> _queue;
    private List<AgentViewModel> _agents;
    private List<AgentViewModel> _availableAgents;
    private TeamViewModel _currentShiftingTeam;
    private IMockData _mockData;

    public ChatQueueService(IMockData mockData)
    {
        _queue = new Queue<ChatSessionViewModel>();
        _mockData = mockData;        
    }

    public bool AddToQueue(string sessionId)
    {
        try
        {
            SetAgents();

            if (_availableAgents == null || !_availableAgents.Any())
            {
                return false; // No available agents
            }

            int queueCapacity = _currentShiftingTeam.Capacity;

            if (_queue.Count >= queueCapacity)
            {
                return false;
            }

            var newSession = new ChatSessionViewModel { SessionId = sessionId };
            _queue.Enqueue(newSession);

            return true; // Successfully added to queue
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception Error: {ex.ToString()}");
            return false;
        }
    }

    private void SetAgents()
    {
        var availableAgents = _mockData.AvailableAgents();
        _currentShiftingTeam = _mockData.GetCurrentShiftingTeam();

        if (availableAgents.Any())
        {
            _agents = _mockData.GetAllAgentList().OrderBy(agent => (int)agent.Seniority).ToList();
            _availableAgents = availableAgents.OrderBy(agent => (int)agent.Seniority).ToList();
        }
    }

    public void MonitorQueue()
    {
        while (true)
        {
            foreach (var session in _queue)
            {
                if ((DateTime.Now - session.PolledDate).TotalSeconds > 3)
                {
                    session.IsActive = false;
                    _queue.Dequeue();
                }
            }
            Thread.Sleep(1000); // Poll every 1 second
        }
    }

    public void AssignChats()
    {
        while (_queue.Any())
        {
            foreach (var agent in _availableAgents)
            {
                //if (agent.Capacity > 0 && _queue.TryDequeue(out var session))
                //{
                //    session.AgentId = agent.Id;
                //    _agents.Capacity--;
                //}
            }
        }
    }

}
