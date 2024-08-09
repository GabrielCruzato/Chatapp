namespace Parrot.Model
{
    public class Vote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? ThreadId { get; set; }
        public Thread Thread { get; set; }
        public int? CommentId { get; set; }
        public Comment Comment { get; set; }
        public bool IsUpvote { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
