using System;
using System.Collections.Generic;

namespace PEP.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public DateTime PostTime { get; set; }

    public string PostContent { get; set; } = null!;

    public bool PostType { get; set; }

    public string? AvatarImgurl { get; set; }

    public int? ProblemId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual AlgorithmProblem? Problem { get; set; }

    public virtual User User { get; set; } = null!;
}
