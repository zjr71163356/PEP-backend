using System;
using System.Collections.Generic;

namespace PEP.Models;

public partial class UserCourse
{
    public int UserCourseId { get; set; }

    public int? UserId { get; set; }

    public int? CourseId { get; set; }

    public bool? IsFavorite { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? User { get; set; }
}
