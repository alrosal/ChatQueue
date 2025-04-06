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

}
