namespace Parrot.Model
{
    public class ModerationLog
    {
        public int Id { get; set; }
        public int ModeratorId { get; set; }
        public User Moderator { get; set; }
        public string Action { get; set; } = string.Empty; 
        public string Reason { get; set; } = string.Empty;
        public DateTime ActionedAt { get; set; } = DateTime.UtcNow;
    }
}
