using System;
using System.Collections.Generic;

namespace PEP.Models;

public partial class Reply
{
    public int ReplyId { get; set; }

    public int CommentId { get; set; }

    public int FromUserId { get; set; }

    public string FromUsername { get; set; } = null!;

    public int ToUserId { get; set; }

    public string ToUsername { get; set; } = null!;

    public string ReplyContent { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string? AvatarImgurl { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User FromUser { get; set; } = null!;

    public virtual User ToUser { get; set; } = null!;
}
