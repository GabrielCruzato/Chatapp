namespace Parrot.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public int ThreadId { get; set; }
        public Thread? Thread { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public bool IsDeleted { get; set; } = false;
        public bool IsEdited { get; set; } = false;
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
        public DateTime? EditedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}