using System;
using System.Collections.Generic;

namespace PEP.Model.Domain;

public partial class CourseTag
{
    public int TagId { get; set; }

    public int CourseId { get; set; }

    public string TagName { get; set; } = null!;

    public int TagColor { get; set; }

    public virtual Course Course { get; set; } = null!;
}
