using System;
using System.Collections.Generic;

namespace PEP.Models.Domain;

public partial class Comment
{
    public int CommentId { get; set; }

    public int PostId { get; set; }

    public int FromUserId { get; set; }

    public string FromUsername { get; set; } = null!;

    public string CommentContent { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public virtual User FromUser { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();
}
