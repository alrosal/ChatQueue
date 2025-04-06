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

            int queueCapacity = Convert.ToInt16(Math.Floor((double)_currentShiftingTeam.Capacity * Constants.TEAM_CAPACITY_MULTIPLIER));

            Console.WriteLine($"Requesting Agent for Session ID: {sessionId} with Team Capacity of {queueCapacity} at {DateTime.Now}.");

            if (_queue.Count >= queueCapacity)
            {
                // Add Overflow Team if Office Hours
                if (DateTime.Now.Hour >= Constants.OFFICE_HOUR_START && DateTime.Now.Hour <= Constants.OFFICE_HOUR_END)
                {
                    _availableAgents.AddRange(_agents.Where(o => o.Team.IsOverflowTeam));
                }
                else
                {
                    return false;
                }
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

    public void PollQueue()
    {
        while (_queue.Any())
        {
            foreach (var session in _queue)
            {
                if ((DateTime.Now - session.PolledDate).TotalSeconds > 3)
                {
                    _queue.Dequeue();
                    Console.WriteLine($"Session {session.SessionId} was not assigned. Removed from Queue.");

                    if (_queue == null || _queue.Count < 1)
                        break;
                }
            }
        }
    }

    public void AssignToAgent()
    {
        while (_queue.Any())
        {
            var agent = GetNextAvailableAgent();

            if (agent != null)
            {
                var session = _queue.Dequeue();
                agent.AssignedSessions = agent.AssignedSessions + 1;
                Console.WriteLine($"Session {session.SessionId} assigned to Agent {agent.Id} : {agent.AssignedSessions} assigned sessions with capacity of {agent.Capacity} at {DateTime.Now}.");
            }
            else
            {
                Console.WriteLine("No available agents.");
                break;
            }
        }
    }

    private AgentViewModel GetNextAvailableAgent()
    {
        var nextAvailableAgent = _availableAgents.FirstOrDefault(agent => agent.AssignedSessions < agent.Capacity);
        if (nextAvailableAgent != null)
        {
            Console.WriteLine($"Next available agent is Agent ID-{nextAvailableAgent.Id} with {nextAvailableAgent.Capacity - nextAvailableAgent.AssignedSessions} remaining capacity.");
            return nextAvailableAgent;
        }
        else
        {
            Console.WriteLine("No available agents.");
            return null;
        }
    }

}
