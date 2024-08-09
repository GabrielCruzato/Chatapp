namespace Parrot.Model
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SubredditId { get; set; }
        public Community? Community { get; set; }
        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    }
}
