namespace Parrot.Model
{
    public class Thread
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool isImagePost { get; set; } = false;
        public string ImageURL {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<User> Moderators { get; set; } = new List<User>(); 
        public string Url { get; set; } = string.Empty;
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
