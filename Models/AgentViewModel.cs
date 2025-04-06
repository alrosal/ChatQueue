namespace ChatQueue.Models;

public class AgentViewModel
{
    public enum SeniorityLevel
    {
        Junior = 1,
        MidLevel = 2,
        Senior = 3,
        TeamLead = 4
    }

    public int Id { get; set; }

    public TeamViewModel Team { get; set; }

    public SeniorityLevel Seniority { get; set; }

    public int Capacity => Seniority switch
    {
        SeniorityLevel.Junior => 4,
        SeniorityLevel.MidLevel => 6,
        SeniorityLevel.Senior => 8,
        SeniorityLevel.TeamLead => 5,
        _ => 4 // Default capacity for Junior seniority
    };


    public int AssignedSessions { get; set; }

}
