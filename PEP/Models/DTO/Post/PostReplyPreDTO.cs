using PEP.Models.DTO.User;

namespace PEP.Models.DTO.Post
{
    public class PostReplyPreDTO
    {
        public int ReplyId { get; set; }

        public int CommentId { get; set; }

        public int FromUserId { get; set; }

        public string FromUsername { get; set; } = null!;

        public int ToUserId { get; set; }

        public string ToUsername { get; set; } = null!;

        public string ReplyContent { get; set; } = null!;

        public DateTime Timestamp { get; set; }

        public virtual UserAvatarDTO FromUser { get; set; } = null!;


    }
}
