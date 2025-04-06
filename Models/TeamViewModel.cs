namespace ChatQueue.Models{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ShiftHourStart { get; set; }
        public int ShiftHourEnd { get; set; }        
        public int Capacity { get; set; }
        public bool IsOverflowTeam { get; set; } = false;
    }
}
