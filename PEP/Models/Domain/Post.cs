using System;
using System.Collections.Generic;

namespace PEP.Models.Domain;

public partial class Post
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime PostTime { get; set; }

    public string PostContent { get; set; } = null!;

    public bool PostType { get; set; }

    public int ProblemId { get; set; }

    public int Likes { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual AlgorithmProblem Problem { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserLike> UserLikes { get; set; } = new List<UserLike>();
}
