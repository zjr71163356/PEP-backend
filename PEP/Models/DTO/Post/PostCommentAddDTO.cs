namespace PEP.Models.DTO.Post
{
    public class PostCommentAddDTO
    {
        public int PostId { get; set; }

        public int FromUserId { get; set; }

        public string FromUsername { get; set; } = null!;

        public string CommentContent { get; set; } = null!;

        public DateTime Timestamp { get; set; }
    }
}
