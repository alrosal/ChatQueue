using ChatQueue.Models;

namespace ChatQueue.Mocker
{
    public interface IMockData
    {
        public List<AgentViewModel> GetAllAgentList();
        public List<AgentViewModel> AvailableAgents();
        public TeamViewModel GetCurrentShiftingTeam();
        public List<TeamViewModel> GetTeamList();
    }
}
