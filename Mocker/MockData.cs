using ChatQueue.Models;
using static ChatQueue.Models.AgentViewModel;

namespace ChatQueue.Mocker;

public class MockData : IMockData
{

    public List<AgentViewModel> GetAllAgentList()
    {
        var teams = GetTeamList();

        return new List<AgentViewModel>
        {
            new AgentViewModel
            {
                Id = 1,
                Team = teams.FirstOrDefault(o => o.Id == 1),
                Seniority = SeniorityLevel.TeamLead
            },
            new AgentViewModel
            {
                Id = 2,
                Team = teams.FirstOrDefault(o => o.Id == 1),
                Seniority = SeniorityLevel.MidLevel
            },
            new AgentViewModel
            {
                Id = 3,
                Team = teams.FirstOrDefault(o => o.Id == 1),
                Seniority = SeniorityLevel.MidLevel
            },
            new AgentViewModel
            {
                Id = 4,
                Team = teams.FirstOrDefault(o => o.Id == 1),
                Seniority = SeniorityLevel.Junior
            },
            new AgentViewModel
            {
                Id = 5,
                Team = teams.FirstOrDefault(o => o.Id == 2),
                Seniority = SeniorityLevel.Senior
            },
            new AgentViewModel
            {
                Id = 6,
                Team = teams.FirstOrDefault(o => o.Id == 2),
                Seniority = SeniorityLevel.MidLevel
            },
            new AgentViewModel
            {
                Id = 7,
                Team = teams.FirstOrDefault(o => o.Id == 2),
                Seniority = SeniorityLevel.MidLevel
            },
            new AgentViewModel
            {
                Id = 8,
                Team = teams.FirstOrDefault(o => o.Id == 2),
                Seniority = SeniorityLevel.Junior
            },
            new AgentViewModel
            {
                Id = 9,
                Team = teams.FirstOrDefault(o => o.Id == 2),
                Seniority = SeniorityLevel.Junior
            },
            new AgentViewModel
            {
                Id = 10,
                Team = teams.FirstOrDefault(o => o.Id == 3),
                Seniority = SeniorityLevel.MidLevel
            },
            new AgentViewModel
            {
                Id = 11,
                Team = teams.FirstOrDefault(o => o.Id == 3),
                Seniority = SeniorityLevel.MidLevel
            },
            new AgentViewModel
            {
                Id = 12,
                Team = teams.FirstOrDefault(o => o.Id == 4),
                Seniority = SeniorityLevel.Junior
            },
            new AgentViewModel
            {
                Id = 13,
                Team = teams.FirstOrDefault(o => o.Id == 4),
                Seniority = SeniorityLevel.Junior
            },
            new AgentViewModel
            {
                Id = 14,
                Team = teams.FirstOrDefault(o => o.Id == 4),
                Seniority = SeniorityLevel.Junior
            },
            new AgentViewModel
            {
                Id = 15,
                Team = teams.FirstOrDefault(o => o.Id == 4),
                Seniority = SeniorityLevel.Junior
            },
            new AgentViewModel
            {
                Id = 16,
                Team = teams.FirstOrDefault(o => o.Id == 4),
                Seniority = SeniorityLevel.Junior
            },
            new AgentViewModel
            {
                Id = 17,
                Team = teams.FirstOrDefault(o => o.Id == 4),
                Seniority = SeniorityLevel.Junior
            }
        };
    }

    public List<AgentViewModel> AvailableAgents()
    {
        var agentList = new List<AgentViewModel>();
        var allAgents = GetAllAgentList();

        var currentTeam = GetCurrentShiftingTeam();

        if (currentTeam != null)
        {
            agentList = allAgents.Where(a => a.Team.Id == currentTeam.Id).ToList();

        }

        return agentList;
    }

    public TeamViewModel GetCurrentShiftingTeam()
    {
        var currentHour = DateTime.Now.Hour;
        var teams = GetTeamList();

        var currentTeam = teams.FirstOrDefault(o => o.ShiftHourStart <= currentHour && currentHour <= o.ShiftHourEnd && o.IsOverflowTeam == false);

        return currentTeam;
    }

    public List<TeamViewModel> GetTeamList()
    {
        return new List<TeamViewModel>
        {
            new TeamViewModel
            {
                Id = 1,
                Name = "Team A",
                ShiftHourStart = 8,
                ShiftHourEnd = 16,
                Capacity = 21
            },
            new TeamViewModel
            {
                Id = 2,
                Name = "Team B",
                ShiftHourStart = 16,
                ShiftHourEnd = 24,
                Capacity = 22
            },
            new TeamViewModel
            {
                Id = 3,
                Name = "Team C",
                ShiftHourStart = 0,
                ShiftHourEnd = 8,
                Capacity = 12
            },
            new TeamViewModel
            {
                Id = 4,
                Name = "Overflow Team",
                ShiftHourStart = 0,
                ShiftHourEnd = 24,
                Capacity = 24,
                IsOverflowTeam = true
            }
        };
    }

}
