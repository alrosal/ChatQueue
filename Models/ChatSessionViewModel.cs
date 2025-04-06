namespace ChatQueue.Models;

public class ChatSessionViewModel
{
    public string SessionId { get; set; }
    public int AgentId { get; set; }
    public bool IsActive { get; set; }
    public DateTime PolledDate { get; set; } = DateTime.Now;

}
