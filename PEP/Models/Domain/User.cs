using System;
using System.Collections.Generic;

namespace PEP.Model.Domain;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Avatar { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Reply> ReplyFromUsers { get; set; } = new List<Reply>();

    public virtual ICollection<Reply> ReplyToUsers { get; set; } = new List<Reply>();

    public virtual ICollection<SubmissionRecord> SubmissionRecords { get; set; } = new List<SubmissionRecord>();

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
