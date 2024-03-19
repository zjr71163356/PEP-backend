using System;
using System.Collections.Generic;

namespace PEP.Models.Domain;

public partial class UserLike
{
    public int LikeId { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
