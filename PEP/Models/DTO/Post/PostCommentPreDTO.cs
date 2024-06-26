﻿using PEP.Models.DTO.Courses.Both;
using PEP.Models.DTO.User;

namespace PEP.Models.DTO.Post
{
    public class PostCommentPreDTO
    {
        public int CommentId { get; set; }

        public int PostId { get; set; }

        public int FromUserId { get; set; }

        public string FromUsername { get; set; } = null!;

        public string CommentContent { get; set; } = null!;

        public DateTime Timestamp { get; set; }
        public virtual ICollection<PostReplyPreDTO> Replies { get; set; } = new List<PostReplyPreDTO>();
        public virtual UserAvatarDTO FromUser { get; set; } = null!;
    }
}
